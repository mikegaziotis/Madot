using ConsoleAppFramework;
using Madot.Interface.API;

namespace Madot.Interface.CLI.CommandHandlers;

public record struct ApiGetCommandArgs(string Id);

public class ApiGetCommandHandler(
    IAPIApi apiClient)
{
    private async Task Handle(ApiGetCommandArgs args)
    {
        Console.WriteLine("here!");
        Api? api;
        try
        {
            api = await apiClient.ApiGetByIdAsync(args.Id);
            Console.WriteLine(api);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static async void Send([FromServices] ApiGetCommandHandler handler, string id)
    {
        await handler.Handle(new ApiGetCommandArgs(id));
    }

}