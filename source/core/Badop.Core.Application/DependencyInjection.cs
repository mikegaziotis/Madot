using System.Diagnostics;
using System.Reflection;
using Badop.Core.Application.Enums;
using Badop.Core.Application.Operations.Commands;
using Badop.Core.Application.Operations.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Badop.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationContext(this IServiceCollection services)
    {
        return services
            .AddScoped<BadopDbContext>()
            .AddCommands()
            .AddQueries();

    }
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services
            .RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(), typeof(ICommandHandler<>),Scope.Transient)
            .RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(),typeof(ICommandHandler<,>),Scope.Transient);
    }
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services
            .RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(),typeof(IQueryHandler<,>),Scope.Transient);
    }

    public static IServiceCollection RegisterTypesByGenericInterface(this IServiceCollection services, Assembly assembly, Type genericInterface, Scope scope)
    {
        var types = assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericType: false } &&
                           type.ImplementsGenericInterface(genericInterface));
        foreach (var itemType in types)
        {
            if (scope == Scope.Transient)
                services.AddTransient(itemType.GetInterfaces()[0], itemType)
                    .AddTransient(itemType,itemType);
            else if (scope == Scope.Scoped)
                services.AddScoped(itemType.GetInterfaces()[0], itemType)
                    .AddScoped(itemType,itemType);
            else if (scope == Scope.Singleton)
                services.AddSingleton(itemType.GetInterfaces()[0], itemType)
                    .AddSingleton(itemType,itemType);
            else
                throw new UnreachableException("Shouldn't be here mate");
        }
               
        return services;
    }

    private static bool ImplementsGenericInterface(this Type current, Type genericType)
    {
        return current.GetInterfaces().ToList()
            .Exists(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
    }
    
}