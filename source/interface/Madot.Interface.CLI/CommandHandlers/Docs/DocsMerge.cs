using System.Text;
using System.Text.Json;
using ConsoleAppFramework;
using JsonDiffPatchDotNet;
using Madot.Interface.API;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using File = System.IO.File;
using FileTuple = (string FileName, string Path, bool IsGuide, string? Contents);
using GuideTuple = (int OrderId, string FormattedName);
using GuideEntry = (Madot.Interface.API.Guide Guide, Madot.Interface.API.GuideVersion LatestVersion);

namespace Madot.Interface.CLI.CommandHandlers;

public record struct DocsMergeCommandArgs(string ApiId, string DocsPath):ICommandArgs;

public class DocsMergeCommandHandler(
    ILogger<DocsMergeCommandHandler> logger, 
    IHomepageApi homepageApi,
    IChangelogApi changelogApi,
    IOpenAPISpecificationApi oasApi,
    IGuideApi guideApi,
    IGuideVersionApi guideVersionApi):BaseCommandHandler<DocsMergeCommandArgs>(logger)
{
    private const string ValidOpenApiFileName = "openapi.json";
    private const string ValidChangelogFileName = "changelog.md";
    private const string ValidHomepageFileName = "homepage.md";

    private string _apiId=string.Empty;
    public override async Task Handle(DocsMergeCommandArgs args)
    {
        try
        {
            _apiId = args.ApiId;
            
            //step 1. load all files from directory  and validate if crucial files are missing 
            // exit if load and validation fails
            if(!LoadAndValidateFiles(args, out List<FileTuple> files, out List<FileTuple> guideFiles))
                return;

            //step 2. Load latest files from Api
            var oasTask =  SafeHttpExecuteAsync(async ()=> await oasApi.OpenApiSpecGetLatestByApiIdAsync(_apiId));
            var homepageTask =  SafeHttpExecuteAsync(async ()=> await homepageApi.HomepageGetLatestByApiIdAsync(_apiId));
            var changelogTask =  SafeHttpExecuteAsync(async ()=> await changelogApi.ChangelogGetLatestByApiIdAsync(_apiId));
            var guidesTask = SafeHttpExecuteAsync(async ()=> await guideApi.GuidesGetByApiIdAsync(_apiId,true));
            var apiLoadTasks = new Task[] { oasTask, homepageTask, changelogTask,guidesTask }; 
            await Task.WhenAll(apiLoadTasks);

            var oas = oasTask.Result;
            var homepage = homepageTask.Result;
            var changelog = changelogTask.Result;
            var guides = guidesTask.Result??[];
            
            
            var guideTasks = guides.ToList().ConvertAll(guide => 
                Task.Run(async () =>
                {
                    var guideVersion = await guideVersionApi.GuideVersionLatestGetByGuideIdAsync(guide.Id);
                    return new GuideEntry(guide, guideVersion);
                }));
            var guideTaskResults = await Task.WhenAll(guideTasks);
            
            //step 3. read files from disk
            var allFiles = new List<FileTuple>();
            allFiles.AddRange(files);
            allFiles.AddRange(guideFiles);
            var diskReadTasks = allFiles.ConvertAll(file => 
                Task.Run(async () => 
                {
                    var content = await File.ReadAllTextAsync(file.Path);
                    file.Contents = content;
                    return file;
                }));

            var diskReadResults = await Task.WhenAll(diskReadTasks);

            // Step 4. Merge Docs
            var apiUpdateTasks = new List<Task<(bool,string)>>();
            foreach (var diskReadResult in diskReadResults)
            {
                GuideTuple guideTuple = new GuideTuple();
                if (diskReadResult.IsGuide)
                    guideTuple = FormatGuideName(diskReadResult.FileName);
                var task = diskReadResult.FileName.ToLower() switch
                {
                    ValidChangelogFileName => CompareAndUpdateChangelog(diskReadResult, changelog),
                    ValidOpenApiFileName => CompareAndUpdateOpenApiSpec(diskReadResult, oas),
                    ValidHomepageFileName => CompareAndUpdateHomepage(diskReadResult, homepage),
                    _ => CompareAndUpdateGuide(guideTuple, diskReadResult, guideTaskResults.FirstOrDefault(x=>x.Guide.Title.Equals(guideTuple.FormattedName)))
                };
                apiUpdateTasks.Add(task);
            }

            var finalResults = await Task.WhenAll(apiUpdateTasks);
            if (finalResults.All(x => x.Item1))
            {
                foreach (var se in finalResults.Select(x=>x.Item2))
                {
                    Console.ForegroundColor = se.Substring(0, 3) switch
                    {
                        "[I]" => ConsoleColor.Green,
                        "[U]" => ConsoleColor.Blue,
                        "[-]" => ConsoleColor.Gray,
                        _ => ConsoleColor.White
                    };
                    Console.WriteLine(se);
                    Console.ResetColor();
                }
            }
            else
            {
                foreach (var error in finalResults.Where(x=>!x.Item1).Select(x=>x.Item2))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    await Console.Error.WriteLineAsync(error);
                    Console.ResetColor();
                }
            }
            Console.WriteLine("Process Complete");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message,ex);
        }
    }

    private async Task<(bool,string)> CompareAndUpdateGuide(GuideTuple guideTuple, FileTuple diskReadResult, GuideEntry? guideEntry)
    {
        if (!guideEntry.HasValue || guideEntry.Value.Guide is null )
        {
            var guideCreated = await SafeHttpExecuteAsync(async ()=> await guideApi.GuideInsertAsync(new GuideInsertCommand
            {
                ApiId = _apiId,
                Title = guideTuple.FormattedName,
                ProvisionalOrderId = guideTuple.OrderId
            }));
            if (guideCreated is not null)
            {
                var guideVersionCreated = await SafeHttpExecuteAsync(async () =>
                    await guideVersionApi.GuideVersionInsertAsync(new GuideVersionInsertCommand
                    {
                        GuideId = guideCreated.Id,
                        Data = diskReadResult.Contents!
                    }));
                if (guideVersionCreated is not null)
                {
                    return new ValueTuple<bool, string>(true,
                        $"[I] Guide \"{guideTuple.FormattedName}\" not found. Inserted.");
                }
                return new ValueTuple<bool, string>(false,
                    $"[X] !Guide \"{guideTuple.FormattedName}\" inserted, but failed to add GuideVersion");
            }
            return new ValueTuple<bool, string>(false, $"[X] !Failed to insert Guide: \"{guideTuple.FormattedName}\"");
        }
        var guide = guideEntry.Value.Guide;

        bool guideUpdated = false;
        if (guide.IsDeleted || guide.ProvisionalOrderId!=guideTuple.OrderId)
        {
            guideUpdated = await SafeHttpExecuteAsync(async () =>
            {
                await guideApi.GuideUpdateAsync(new GuideUpdateCommand
                {
                    Id = guide.Id,
                    IsDeleted = false,
                    ProvisionalOrderId = guideTuple.OrderId
                });
                return true;
            });
            if (!guideUpdated)
            {
                return new ValueTuple<bool, string>(false, $"[X] !Failed to update Guide: \"{guideTuple.FormattedName}\"");
            }
        }
        var latestGuideVersion = guideEntry.Value.LatestVersion; 
        if (latestGuideVersion.Data != diskReadResult.Contents || latestGuideVersion.IsDeleted)
        {
            var result  = await SafeHttpExecuteAsync(async () =>
            {
                await guideVersionApi.GuideVersionInsertAsync(new GuideVersionInsertCommand
                {
                    GuideId = guide.Id,
                    Data = latestGuideVersion.Data
                });
                return true;
            });
            if (result)
            {
                if (guideUpdated)
                {
                    return new ValueTuple<bool, string>(true, $"[U] Updated Guide: \"{guideTuple.FormattedName}\" and its latest GuideVersion");
                }
                return new ValueTuple<bool, string>(true, $"[U] Updated GuideVersion for Guide: \"{guideTuple.FormattedName}\"");
            }
            return new ValueTuple<bool, string>(false, $"[X] !Failed to update GuideVersion for Guide: \"{guideTuple.FormattedName}\"");
        }
        if(guideUpdated)
            return new ValueTuple<bool, string>(true, $"[U] Updated Guide: \"{guideTuple.FormattedName}\"");
        
        return new ValueTuple<bool, string>(true, $"[-] Guide: \"{guideTuple.FormattedName}\" matched and so did its latest version. Skipped");
    }

    private GuideTuple FormatGuideName(string fileName)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var chars = fileNameWithoutExtension.ToCharArray();
        string orderId = "";
        StringBuilder sb = new StringBuilder();
        bool openingSection = true;
        foreach (var t in chars)
        {
            if (char.IsNumber(t) && openingSection)
            {
                orderId += t;
                continue;
            }
            if (t == '_' && openingSection)
                openingSection = false;
            else if (t == '_')
                sb.Append(' ');
            else
                sb.Append(t);
        }
        return new GuideTuple(int.Parse(orderId),sb.ToString());
    }

    private async Task<(bool,string)> CompareAndUpdateChangelog(FileTuple fileContent, Changelog? changelog)
    {
        if (changelog is not null)
        {
            if (changelog.Data == fileContent.Contents)
            {
                return new ValueTuple<bool, string>(true,"[-] Changelog contents unmodified. Skipped");
            }
            var result = await SafeHttpExecuteAsync(async () =>
            {
                await changelogApi.ChangelogInsertAsync(new VersionedDocumentInsertCommand
                {
                    ApiId = _apiId,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[U] Changelog contents modified. Updated" : "[X] !Failed to update Changelog!");
        }
        else
        {
            var result = await SafeHttpExecuteAsync( async () =>
            {
                await changelogApi.ChangelogInsertAsync(new VersionedDocumentInsertCommand()
                {
                    ApiId = _apiId,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[I] Previous Changelog not found. Inserted" : "![X] Failed to insert Changelog!");
        }
    }
    
    private async Task<(bool,string)> CompareAndUpdateHomepage(FileTuple fileContent, Homepage? homepage)
    {
        if (homepage is not null)
        {
            if (homepage.Data == fileContent.Contents)
            {
                return new ValueTuple<bool, string>(true, "[-] Homepage contents unmodified. Skipped");
            }
            var result = await SafeHttpExecuteAsync(async () =>
            {
                await homepageApi.HomepageInsertAsync(new VersionedDocumentInsertCommand
                {
                    ApiId = _apiId,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[U] Homepage contents modified. Updated" : "![X] Failed to update Homepage!");
        }
        else
        {
            var result = await SafeHttpExecuteAsync( async () =>
            {
                await homepageApi.HomepageInsertAsync(new VersionedDocumentInsertCommand()
                {
                    ApiId = _apiId,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[I] Previous Homepage not found. Inserted" : "![X] Failed to insert Homepage!");
        }
    }
    
    private async Task<(bool, string)> CompareAndUpdateOpenApiSpec(FileTuple fileContent, OpenApiSpec? oas)
    {
        JToken? fileJson;
        try
        {
            if (fileContent.Contents is null)
                throw new ArgumentException();

            fileJson = JToken.Parse(fileContent.Contents);
        }
        catch (Exception ex)
        {
            return new ValueTuple<bool, string>(false, $"[x] !Contents of openapi.json not valid json!");
        }

        if (oas is not null)
        {
            
            var oasJson = JToken.Parse(oas.Data);
            if (JToken.DeepEquals(fileJson,oasJson))
            {
                return new ValueTuple<bool, string>(true,"[-] OpenApiSpec contents unmodified. Skipped");
            }
            var jdp = new JsonDiffPatch();
            Console.WriteLine(jdp.Diff(oasJson, fileJson).ToString());
            var result = await SafeHttpExecuteAsync(async () =>
            {
                await oasApi.OpenApiSpecUpdateAsync(new VersionedDocumentUpdateCommand
                {
                    Id = oas.Id,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[U] OpenApiSpec contents modified. Updated" : "![X] Failed to update OpenApiSpec!");
        }
        else
        {
            var result = await SafeHttpExecuteAsync( async () =>
            {
                await oasApi.OpenApiSpecInsertAsync(new VersionedDocumentInsertCommand()
                {
                    ApiId = _apiId,
                    Data = fileContent.Contents!
                });
                return true;
            });
            return new ValueTuple<bool, string>(result, result ? "[I] Previous OpenApiSpec not found. Inserted" : "![X] Failed to insert OpenApiSpec!");
        }
    }

    private bool ValidateGuides(List<FileTuple> guides)
    {
        bool error = false;
        foreach (var guide in guides)
        {
            if(!Path.GetExtension(guide.FileName).Equals(".md", StringComparison.OrdinalIgnoreCase))
            {
                if (!error)
                {
                    Console.Error.WriteLine("All guides should be markdown files");
                }
                Console.Error.WriteLine($"File '{guide.FileName}' is not an .md file");
                error = true;
            }
        }
        return !error;
    }

    private bool ValidateFiles(List<FileTuple> files)
    {
        List<string> validNames = [ValidChangelogFileName, ValidOpenApiFileName, ValidHomepageFileName];
        
        var errors = new Queue<string>();
        if (!files.Any(x => x.FileName.Equals(ValidOpenApiFileName, StringComparison.Ordinal)))
            errors.Enqueue("- Did not find openapi.json");
        if (!files.Any(x => x.FileName.Equals(ValidHomepageFileName, StringComparison.Ordinal)))
            errors.Enqueue("- Did not find homepage.md");
        if (errors.Count > 0)
        {
            Console.Error.WriteLine("Missing Files!");
            while (errors.Count > 0)
            {
                Console.Error.WriteLine(errors.Dequeue());
            }

            return false;
        }

        files.RemoveAll(x => !validNames.Contains(x.FileName.ToLower()));
        return true;
    }

    private bool LoadAndValidateFiles(DocsMergeCommandArgs args, out List<FileTuple> files, out List<FileTuple> guides)
    {
        files = [];
        guides = [];

        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(currentDirectory, args.DocsPath);
            fullPath = Path.GetFullPath(fullPath);
            files = GetFilesFromPath(fullPath,false);

            if (!ValidateFiles(files))
                return false;

            var guidesFolder = Directory.GetDirectories(fullPath).ToList().FirstOrDefault(x =>
                string.Equals(Path.GetFileName(x), "guides", StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(guidesFolder))
                guides = GetFilesFromPath(guidesFolder,true);

            if (guides.Count != 0 && !ValidateGuides(guides))
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return false;
        }
    }

    private List<FileTuple> GetFilesFromPath(string path, bool isGuidePath)
    {
        var files = Directory.GetFiles(path);
        var fileList = new List<FileTuple>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            fileList.Add((fileName, file, isGuidePath, null));
        }
        return fileList;
    }

    public static async Task Send([FromServices] DocsMergeCommandHandler handler, string apiId, string docsPath)
    {
        await handler.Handle(new DocsMergeCommandArgs(apiId, docsPath));
    }
}