using System.Reflection;
using Madot.Core.Application;
using Madot.Core.Application.Enums;

namespace Madot.Interface.WebAPI.Endpoints;

public static class DependencyInjection
{
    public static void AddEndpointHandlers(this IServiceCollection services)
    {
        services.RegisterTypesByGenericInterface(Assembly.GetExecutingAssembly(),typeof(IEndpoint<,>),Scope.Transient);
    }
}

public static partial class EndpointExtensions
{
    #region ResourceNames 
    private const string ApiTag = "API";
    private const string ApiVersionTag = "API Version";
    private const string AppCommonPageTag = "App Common Pages";
    private const string ChangelogTag = "Changelog";
    private const string DocumentStatusTag = "Document Status";
    private const string HomepageTag = "Homepage";
    private const string OpenApiSpecTag = "Open API Specification";
    private const string GuideTag = "Guide";
    private const string GuideVersionTag = "Guide Version";
    private const string FileTag = "File";
    #endregion 
}