namespace Badop.Shell.API.Handlers;

public static class DependencyInjection
{
    public static void RegisterHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ApiGetHandler>();
        serviceCollection.AddScoped<ApiPostHandler>();
        serviceCollection.AddScoped<ApiPatchHandler>();
    }
}