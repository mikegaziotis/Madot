using Badop.Core.Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Badop.Infrastrupture.DataConfig;

public static class DependencyInjection
{
    static void AddDatabaseConfiguration(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDatabaseConfigurationProvider, DatabaseConfigurationProvider>();
    }
}