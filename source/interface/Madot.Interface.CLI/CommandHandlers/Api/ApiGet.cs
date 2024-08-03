using System.Text.Json;
using ConsoleAppFramework;
using Madot.Interface.API;
using Madot.Interface.CLI.Responses;
using Microsoft.Extensions.Logging;
using Refit;

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiGetCommandArgs(string Id):ICommandArgs;

public class ApiGetCommandHandler(IAPIApi apiClient, ILogger<ApiGetCommandHandler> logger):BaseCommandHandler<ApiGetCommandArgs>(logger)
{
    
    public override async Task Handle(ApiGetCommandArgs args)
    {
        var api = await SafeHttpExecuteAsync(async () => await apiClient.ApiGetByIdAsync(args.Id));
        if (api is null)
        {
            Console.WriteLine(JsonSerializer.Serialize(new NotFoundResponse($"!API with Id: '{args.Id}' not found!"), DefaultSerializerOptions()));
            Environment.Exit(0);         
        }
        Console.WriteLine(JsonSerializer.Serialize(api, DefaultSerializerOptions()));
        Environment.Exit(0);
    }

    public static async Task Send([FromServices] ApiGetCommandHandler handler, string id)
    {
        await handler.Handle(new ApiGetCommandArgs(id));
    }
}