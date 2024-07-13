using System.Diagnostics;
using System.Reflection;
using Madot.Core.Application.Enums;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Madot.Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationContext(this IServiceCollection services)
    {
        return services
            .AddScoped<MadotDbContext>()
            .AddProviders()
            .AddQueries()
            .AddCommands();

    }

    private static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services
            .AddSingleton<IKeyProvider, KeyProvider>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services
            .RegisterTypesByAbstractClassImplementation(Assembly.GetExecutingAssembly(), typeof(ICommandHandler<,>),Scope.Transient)
            .RegisterTypesByAbstractClassImplementation(Assembly.GetExecutingAssembly(),typeof(ICommandHandler<,,>),Scope.Transient);
    }
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services
            .RegisterTypesByAbstractClassImplementation(Assembly.GetExecutingAssembly(),typeof(IQueryHandler<,>),Scope.Transient);
    }

    public static IServiceCollection RegisterTypesByGenericInterface(this IServiceCollection services, Assembly assembly, Type genericInterface, Scope scope)
    {
        var types = assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericType: false } && type.ImplementsGenericInterface(genericInterface));
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

    public static IServiceCollection RegisterTypesByAbstractClassImplementation(this IServiceCollection services,
        Assembly assembly, Type abstractType, Scope scope)
    {
        var types = assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericType: false } && type.ImplementsAbstractType(abstractType));
        foreach (var itemType in types)
        {
            if (scope == Scope.Transient)
                services.AddTransient(itemType.BaseType!, itemType)
                    .AddTransient(itemType,itemType);
            else if (scope == Scope.Scoped)
                services.AddScoped(itemType.BaseType!, itemType)
                    .AddScoped(itemType,itemType);
            else if (scope == Scope.Singleton)
                services.AddSingleton(itemType.BaseType!, itemType)
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
    
    private static bool ImplementsAbstractType(this Type current, Type abstractType)
    {
        if (current.BaseType is null)
            return false;
        
        var result=  current.BaseType.IsAbstract && current.BaseType.IsGenericType && current.BaseType.GetGenericTypeDefinition() == abstractType;
        return result;
    }
    
}