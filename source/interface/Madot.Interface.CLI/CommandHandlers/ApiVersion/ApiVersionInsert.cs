using System.Text.RegularExpressions;
using ConsoleAppFramework;
using Madot.Interface.API;
using Microsoft.Extensions.Logging;

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiVersionPublishCommandArgs(string ApiId, string VersionNumber, bool AutoIncrement, bool UpdateLatest):ICommandArgs;

public class ApiVersionPublishCommandHandler(
    IAPIVersionApi apiVersionClient, 
    IChangelogApi changelogClient,
    IHomepageApi homepageClient,
    IOpenAPISpecificationApi oasClient,
    IGuideApi guideClient,
    IGuideVersionApi guideVersionClient,
    ILogger<ApiVersionPublishCommandHandler> logger):BaseCommandHandler<ApiVersionPublishCommandArgs>(logger)
{
    private int _majorVersion = 1;
    private int _minorVersion = 0;
    private string _apiId = null!;
    public override async Task Handle(ApiVersionPublishCommandArgs args)
    {
        _apiId = args.ApiId;
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
            Action.Explicit => await InsertExplicit(),
            Action.AutoIncrement => await AutoIncrement(lastVersion),
            Action.UpdateLatest => await UpdateLatest(lastVersion!),
            _ => false
        };
        if (!success)
        {
            Environment.Exit(1);
            return;
        }
        Environment.Exit(0);
    }

    private async Task<bool> UpdateLatest(ApiVersion lastVersion)
    {
        //var latestChangelogId = GetLatestChangelog();
        var command = new ApiVersionUpdateCommand
        {
            Id = lastVersion.Id,
            BuildOrReleaseTag = lastVersion.BuildOrReleaseTag,
            OpenApiSpecId = lastVersion.OpenApiSpecId,
            HomepageId = lastVersion.HomepageId,
            ChangelogId = lastVersion.ChangelogId,
            IsBeta = lastVersion.IsBeta,
            IsHidden = lastVersion.IsHidden,
            GuideVersionItems = lastVersion.GuideVersionItems,
            FileItems = lastVersion.FileItems
        };
        return await SafeHttpExecuteAsync(async () =>
        {
            await apiVersionClient.ApiVersionUpdateAsync(command);
            return true;
        });
    }

    private async Task<bool> AutoIncrement(ApiVersion? lastVersion)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> InsertExplicit()
    {
        throw new NotImplementedException();
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


    public static async Task Send([FromServices] ApiVersionPublishCommandHandler handler, string apiId, string versionNumber = "", bool autoIncrement=false, bool updateLatest = false)
    {
        await handler.Handle(new ApiVersionPublishCommandArgs(apiId,versionNumber,autoIncrement,updateLatest));
    }

    private enum Action
    {
        Unknown,
        Explicit,
        AutoIncrement,
        UpdateLatest
    }
}