using System.Text.Json.Serialization;
using Madot.Core.Application;
using Madot.Core.Application.Providers;
using Madot.Infrastructure.SqlServer;
using Madot.Interface.WebAPI.Automapper;
using Madot.Interface.WebAPI.Endpoints;
using Madot.Interface.WebAPI.Providers;
using Madot.Interface.WebAPI.Swagger;

namespace Madot.Interface.WebAPI;

public static class DependencyInjection
{
    public static void LoadAppConfiguration(this IServiceCollection services, ConfigurationManager conf)
    {
        conf.AddJsonFile("appsettings.json", true, true);
        switch (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
        {
            case "Local":
                Console.WriteLine("We are LOCAL!");
                conf.AddJsonFile("appsettings.Local.json", true, true);
                break;
            case "Docker":
                Console.WriteLine("We are DOCKER!");
                conf.AddJsonFile("appsettings.Docker.json", true, true);
                break;
        }
        services.Configure<DatabaseOptions>(conf.GetSection(DatabaseOptions.SectionName));
    }
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddDatabaseServices();
        services.AddApplicationServices();
        
        //Web Api Services below
        services.AddAutomapper();
        services.AddSwaggerGen(o=>o.DocumentFilter<DocumentFilter>());
        services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.AddTransient<IUserProvider, UserProvider>();
        services.AddEndpointHandlers();
        services.ConfigureHttpJsonOptions(options => {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
        });
    }

    public static void AddMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.AddRouting();
    }
}