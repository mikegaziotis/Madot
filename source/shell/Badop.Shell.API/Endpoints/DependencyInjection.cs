using System.Reflection;
using Badop.Core.Application;
using Badop.Core.Application.Enums;

namespace Badop.Shell.API.Endpoints;

public static class DependencyInjection
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(),typeof(IMediator<,>),Scope.Transient);
    }
}

public static partial class EndpointExtensions
{
    #region ResourceNames 
    private const string ApiTag = "API";
    private const string ApiVersionTag = "API Version";
    private const string ChangelogTag = "Changelog";
    private const string HomepageTag = "Hopemage";
    private const string OpenApiSpecTag = "Open API Specification";
    private const string GuideTag = "Guide";
    private const string GuideVersionTag = "Guide Version";
    private const string FileTag = "File";
    #endregion 
}