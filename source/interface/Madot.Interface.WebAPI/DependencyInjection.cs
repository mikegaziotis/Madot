using Madot.Core.Application.Providers;
using Madot.Interface.WebAPI.Providers;

namespace Madot.Interface.WebAPI;

public static class DependencyInjection
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddTransient<IUserProvider, UserProvider>();
    }
}