using System.Text.Json;
using ConsoleAppFramework;
using Madot.Interface.API;
using Microsoft.Extensions.Logging;
using Refit;

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiVersionPublishCommandArgs(string ApiId, string VersionNumber, bool AutoIncrement, bool UpdateLatest):ICommandArgs;

public class ApiVersionPublishCommandHandler(IAPIApi apiClient, ILogger<ApiVersionPublishCommandHandler> logger):BaseCommandHandler<ApiVersionPublishCommandArgs>(logger)
{
    public override async Task Handle(ApiVersionPublishCommandArgs args)
    {
        Console.WriteLine("Success!");
    }

    public static async Task Send([FromServices] ApiVersionPublishCommandHandler handler, string apiId, string versionNumber = "", bool autoIncrement=false, bool updateLatest = false)
    {
        await handler.Handle(new ApiVersionPublishCommandArgs(apiId,versionNumber,autoIncrement,updateLatest));
    }
}