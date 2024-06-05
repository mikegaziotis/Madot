using Badop.Core.Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Badop.Infrastructure.SqlServer;

public static class DependencyInjection
{
    public static void AddDatabaseConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDatabaseConfigurationProvider, SqlConfigurationProvider>();
    }
}