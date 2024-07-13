using Madot.Core.Application.Providers;
using Madot.Shell.API.Providers;

namespace Madot.Shell.API;

public static class DependencyInjection
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddTransient<IUserProvider, UserProvider>();
    }
}