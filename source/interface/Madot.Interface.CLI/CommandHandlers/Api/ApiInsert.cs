using System.Text.Json;
using ConsoleAppFramework;
using Madot.Interface.API;
using Madot.Interface.CLI.Responses;
using Microsoft.Extensions.Logging;
using Refit;

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiInsertCommandArgs(ApiInsertCommand Command):ICommandArgs;

public class ApiInsertCommandHandler(IAPIApi apiClient, ILogger<ApiInsertCommandHandler> logger):BaseCommandHandler<ApiInsertCommandArgs>(logger)
{
    public override async Task Handle(ApiInsertCommandArgs args)
    {
        var result = await SafeHttpExecuteAsync(async () =>
        {
            await apiClient.ApiInsertAsync(args.Command);
            return true;
        });
        if (result is false)
        {
            Environment.Exit(1);
        }
        Console.WriteLine($"Success! Created api resource with Id: '{args.Command.Id}'");
        Environment.Exit(0);
    }

    public static async Task Send([FromServices] ApiInsertCommandHandler handler, string id, string displayName, string baseUrl, bool isHidden = false, bool isPreview = false, bool isInternal =false, int orderId = 0)
    {
        ApiInsertCommand command = new ApiInsertCommand()
        {
            Id = id,
            DisplayName = displayName,
            BaseUrlPath = baseUrl,
            IsHidden = isHidden,
            IsPreview = isPreview,
            IsInternal = isInternal,
            OrderId = orderId
        };
        await handler.Handle(new ApiInsertCommandArgs(command));
    }
}