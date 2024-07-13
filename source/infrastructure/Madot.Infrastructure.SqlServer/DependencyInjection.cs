using Madot.Core.Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Madot.Infrastructure.SqlServer;

public static class DependencyInjection
{
    public static void AddDatabaseConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDatabaseConfigurationProvider, SqlConfigurationProvider>();
    }
}