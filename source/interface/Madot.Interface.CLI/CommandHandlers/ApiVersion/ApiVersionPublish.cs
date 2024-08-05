using System.Text.Json;
using System.Text.RegularExpressions;
using ConsoleAppFramework;
using Madot.Interface.API;
using Madot.Interface.CLI.Responses;
using Microsoft.Extensions.Logging;
using VersionedDocuments = (string HomepageId, string OasId, string? ChangelogId, System.Collections.Generic.List<Madot.Interface.API.GuideVersionItem> GuideVersionItems);
using File = Madot.Interface.API.File; 

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiVersionPublishCommandArgs(string ApiId, string VersionNumber, bool AutoIncrement, bool UpdateLatest, string? Tag):ICommandArgs;

public class ApiVersionPublishCommandHandler(
    IAPIVersionApi apiVersionClient, 
    IChangelogApi changelogClient,
    IHomepageApi homepageClient,
    IOpenAPISpecificationApi oasClient,
    IGuideApi guideClient,
    IGuideVersionApi guideVersionClient,
    IFileApi fileClient,
    ILogger<ApiVersionPublishCommandHandler> logger):BaseCommandHandler<ApiVersionPublishCommandArgs>(logger)
{
    private int _majorVersion = 1;
    private int _minorVersion = 0;
    private string _apiId = null!;
    private string? _buildOrReleaseTag;
    private string? _apiVersionId;
    public override async Task Handle(ApiVersionPublishCommandArgs args)
    {
        _apiId = args.ApiId;
        _buildOrReleaseTag = args.Tag;
        var result = ValidateArgs(args, out Action action);
        if (!result)
        {
            Environment.Exit(1);
            return;
        }
        var lastVersion = await SafeHttpExecuteAsync(async ()=> 
            await apiVersionClient.ApiVersionGetLatestByApiIdAsync(args.ApiId,true));
        if (lastVersion is null && action == Action.UpdateLatest)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Error.WriteLineAsync("!Error, no previous version found to update!");
            Console.ResetColor();
            Environment.Exit(1);
            return;
        }

        var success = action switch
        {
            Action.Explicit => await InsertExplicit(lastVersion),
            Action.AutoIncrement => await AutoIncrement(lastVersion),
            Action.UpdateLatest => await UpdateLatest(lastVersion!),
            _ => false
        };
        if (!success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Error.WriteLineAsync(JsonSerializer.Serialize(new ErrorResponse("Failed to save ApiVersion"),DefaultSerializerOptions()));
            Console.ResetColor();
            Environment.Exit(1);
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(JsonSerializer.Serialize(new CreatedResponse(_apiVersionId!),DefaultSerializerOptions()));
        Console.ResetColor();
        Environment.Exit(0);
    }

    private async Task<bool> UpdateLatest(ApiVersion lastVersion)
    {
        var latestDocs = await GetLatestVersionedDocs();
        if(latestDocs is null)
            return false;

        var command = new ApiVersionUpdateCommand
        {
            Id = lastVersion.Id,
            BuildOrReleaseTag = _buildOrReleaseTag,
            OpenApiSpecId = latestDocs.Value.OasId,
            HomepageId = latestDocs.Value.HomepageId,
            ChangelogId = latestDocs.Value.ChangelogId,
            IsBeta = lastVersion.IsBeta,
            IsHidden = lastVersion.IsHidden,
            GuideVersionItems = latestDocs.Value.GuideVersionItems,
            FileItems = lastVersion.FileItems
        };
        return await SafeHttpExecuteAsync(async () =>
        {
            await apiVersionClient.ApiVersionUpdateAsync(command);
            _apiVersionId = lastVersion.Id;
            return true;
        });
    }

    private async Task<bool> AutoIncrement(ApiVersion? lastVersion)
    {
        var latestDocs = await GetLatestVersionedDocs();
        if(latestDocs is null)
            return false;

        var command = new ApiVersionInsertCommand
        {
            ApiId = _apiId,
            MajorVersion = lastVersion?.MajorVersion ?? 1,
            MinorVersion = lastVersion?.MinorVersion+1 ?? 0,
            BuildOrReleaseTag = _buildOrReleaseTag,
            OpenApiSpecId = latestDocs.Value.OasId,
            HomepageId = latestDocs.Value.HomepageId,
            ChangelogId = latestDocs.Value.ChangelogId,
            IsBeta = lastVersion?.IsBeta ?? false,
            IsHidden = lastVersion?.IsHidden ?? false,
            GuideVersionItems = latestDocs.Value.GuideVersionItems,
            FileItems = lastVersion?.FileItems ?? []
        };
        return await SafeHttpExecuteAsync(async () =>
        {
            var created = await apiVersionClient.ApiVersionInsertAsync(command);
            _apiVersionId = created.Id;
            return true;
        });
    }

    private async Task<bool> InsertExplicit(ApiVersion? lastVersion)
    {
        var latestDocs = await GetLatestVersionedDocs();
        if(latestDocs is null)
            return false;

        List<FileItem> fileItems = lastVersion?.FileItems.ToList() ?? [];
        if(lastVersion is null)
        {
            var files = await SafeHttpExecuteAsync(async () => await fileClient.FilesGetByApiIdAsync(_apiId));
            if(files is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                await Console.Error.WriteLineAsync("!Error. Failed to retrieve Files for Api Id");
                Console.ResetColor();
                return false;
            }
            int i=0;
            foreach (File file in files){
                i++;
                fileItems.Add(new FileItem{
                    FileId = file.Id,
                    OrderId = i
                });
            }
        }

        var command = new ApiVersionInsertCommand
        {
            ApiId = _apiId,
            MajorVersion = _majorVersion,
            MinorVersion = _minorVersion,
            BuildOrReleaseTag = _buildOrReleaseTag,
            OpenApiSpecId = latestDocs.Value.OasId,
            HomepageId = latestDocs.Value.HomepageId,
            ChangelogId = latestDocs.Value.ChangelogId,
            IsBeta = false,
            IsHidden = false,
            GuideVersionItems = latestDocs.Value.GuideVersionItems,
            FileItems = fileItems
        };
        return await SafeHttpExecuteAsync(async () =>
        {
            var created = await apiVersionClient.ApiVersionInsertAsync(command);
            _apiVersionId = created.Id;
            return true;
        });
    }

    private async Task<VersionedDocuments?> GetLatestVersionedDocs()
    {
        var latestOasTask = SafeHttpExecuteAsync(async () => await oasClient.OpenApiSpecGetLatestByApiIdAsync(_apiId));
        var latestHomepageTask = SafeHttpExecuteAsync(async () => await homepageClient.HomepageGetLatestByApiIdAsync(_apiId));
        await Task.WhenAll([latestOasTask, latestHomepageTask]);
        var latestOas = latestOasTask.Result;
        var latestHomepage = latestHomepageTask.Result;
        if(latestOas is null || latestHomepage is null)
        {
            
            Console.ForegroundColor=ConsoleColor.Red;
            if(latestOas is null)
                await Console.Error.WriteLineAsync("!Error. No OpenApiSpec document found for this Api Id");
            if(latestHomepage is null)
                await Console.Error.WriteLineAsync("!Error. No Homepage document found for this Api Id");
            Console.ResetColor();
            return null;
        }
        var latestChangelogTask = SafeHttpExecuteAsync(async () => await changelogClient.ChangelogGetLatestByApiIdAsync(_apiId));
        var guidesTask = SafeHttpExecuteAsync(async () => await guideClient.GuidesGetByApiIdAsync(_apiId,false));
        await Task.WhenAll([latestChangelogTask, guidesTask]);
        var guides = guidesTask.Result;
        List<GuideVersionItem?> latestGuideVersions=[];
        if(guides is not null && guides.Count > 0)
        {
            latestGuideVersions = (await Task.WhenAll(guides.ToList().ConvertAll(x=>
                SafeHttpExecuteAsync(async () => 
                { 
                    var guideVersion = await guideVersionClient.GuideVersionLatestGetByGuideIdAsync(x.Id);
                    return new GuideVersionItem {
                        GuideVersionId= guideVersion.Id, 
                        OrderId = x.ProvisionalOrderId
                    };
                })))).ToList();
        }
        
        var latestChangelog = latestChangelogTask.Result;
        return new VersionedDocuments(latestHomepage.Id,latestOas.Id, latestChangelog?.Id, 
           latestGuideVersions.Where(x=>x is not null).ToList().ConvertAll(x=>x!));
        
    }

    private bool ValidateArgs(ApiVersionPublishCommandArgs args, out Action action)
    {
        action = Action.Unknown;
        int count = 0;
        if (args.VersionNumber.Length > 0)
        {
            action = Action.Explicit;            
            count++;
        }
        if (args.AutoIncrement)
        {
            action = Action.AutoIncrement;
            count++;
        }
        if (args.UpdateLatest)
        {
            action = Action.UpdateLatest;
            count++;
        }

        if (count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("You must use one of the following:");
            Console.Error.WriteLine("--version-number");
            Console.Error.WriteLine("--auto-increment");
            Console.Error.WriteLine("--update-latest");
            Console.ResetColor();
            return false;
        }
        if (count > 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("You must use ONLY one of:");
            Console.Error.WriteLine("--version-number");
            Console.Error.WriteLine("--auto-increment");
            Console.Error.WriteLine("--update-latest");
            Console.ResetColor();
            return false;
        }

        if (action == Action.Explicit)
        {
            return ValidateVersionNumber(args.VersionNumber);
        }
            
        return true;
    }

    private bool ValidateVersionNumber(string version)
    {
        var regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
        var result = regex.IsMatch(version);
        if (result)
        {
            var splitResult = version.Split('.');
            _majorVersion = int.Parse(splitResult[0]);
            _minorVersion = int.Parse(splitResult[1]);
        }
        return result;
    }

    public static async Task Send([FromServices] ApiVersionPublishCommandHandler handler, string apiId, string versionNumber = "", bool autoIncrement=false, bool updateLatest = false, string? tag = null)
    {
        await handler.Handle(new ApiVersionPublishCommandArgs(apiId,versionNumber,autoIncrement,updateLatest, tag));
    }

    private enum Action
    {
        Unknown,
        Explicit,
        AutoIncrement,
        UpdateLatest
    }
}