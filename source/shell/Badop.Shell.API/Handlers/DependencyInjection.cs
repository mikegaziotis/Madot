using System.Reflection;
using Badop.Core.Application;
using Badop.Core.Application.Enums;

namespace Badop.Shell.API.Handlers;

public static class DependencyInjection
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(),typeof(IHandler<,>),Scope.Transient);
    }
}