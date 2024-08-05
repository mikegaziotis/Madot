using ConsoleAppFramework;
using Madot.Interface.API;
using Madot.Interface.CLI.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Madot.Interface.CLI;

internal static class DependencyInjection
{
    internal static void ConfigureServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddHttpClient();
        services.ConfigureRefitClients();//"http://localhost:5246"
        services.AddCommandHandlers();
    }

    private static void AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ApiGetCommandHandler>();
        services.AddScoped<ApiInsertCommandHandler>();
        services.AddScoped<DocsMergeCommandHandler>();
        services.AddScoped<ApiVersionPublishCommandHandler>();
    }
}