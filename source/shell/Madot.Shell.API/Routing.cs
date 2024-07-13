using Madot.Shell.API.Endpoints;


public static class Routing
{
    public static void AddRouting(this WebApplication app)
    {
        //Api
        #region Api

        app.MapGet("api/{id}", EndpointExtensions.ApiGetByIdEndpoint).ApiGetByIdEndpointConfiguration();

        app.MapGet("api/list", EndpointExtensions.ApiGetAllEndpoint).ApiGetAllEndpointConfiguration();
        
        app.MapGet("api/search-by-name",EndpointExtensions.ApiSearchByNameEndpoint).ApiSearchByNameEndpointConfiguration();

        app.MapPost("api/insert", EndpointExtensions.ApiInsertEndpoint).ApiInsertEndpointConfiguration();
        
        #endregion
        
        //Api Version
        #region Api Version

        app.MapGet("api-version/{id}", EndpointExtensions.ApiVersionGetByIdEndpoint).ApiVersionGetByIdEndpointConfiguration();

        app.MapGet("api-version/latest/{api_id}", EndpointExtensions.ApiVersionGetLatestByApiIdEndpoint).ApiVersionGetLatestByApiIdEndpointConfiguration(); 
        
        app.MapGet("api-version/list/{api_id}", EndpointExtensions.ApiVersionsGetByApiIdEndpoint).ApiVersionsGetByApiIdEndpointConfiguration();
        
        #endregion
        
        //App Static Page
        #region App Static Page

        app.MapGet("app-static-page/{id:int}", EndpointExtensions.AppCommonPageGetByIdEndpoint).AppCommonPageGetByIdEndpointConfiguration();
        
        app.MapGet("app-static-page/list", EndpointExtensions.AppCommonPagesGetAllEndpoint).AppCommonPagesGetAllEndpointConfiguration();
        
        #endregion
        
        //Changelog
        #region Changelog

        app.MapGet("changelog/{id}",EndpointExtensions.ChangelogGetByIdEndpoint).ChangelogGetByIdEndpointConfiguration();

        app.MapGet("changelog/by-api-version/{api_version_id}", EndpointExtensions.ChangelogGetByApiVersionIdEndpoint).ChangelogGetByApiVersionIdEndpointConfiguration();

        app.MapGet("changelog/by-api/{api_id}", EndpointExtensions.ChangelogGetByApiIdEndpoint).ChangelogGetByApiIdEndpointConfiguration();
        #endregion
        
        //Homepage
        #region Homepage

        app.MapGet("homepage/{id}",EndpointExtensions.HomepageGetByIdEndpoint).HomepageGetByIdEndpointConfiguration();

        app.MapGet("homepage/by-api-version/{api_version_id}", EndpointExtensions.HomepageGetByApiVersionIdEndpoint).HomepageGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("homepage/by-api/{api_id}", EndpointExtensions.HomepageGetByApiIdEndpoint).HomepageGetByApiIdEndpointConfiguration();

        #endregion

        //Open Api Specification
        #region Open Api Specification

        app.MapGet("open-api-spec/{id}",EndpointExtensions.OpenApiSpecGetByIdEndpoint).OpenApiSpecGetByIdEndpointConfiguration();
        
        app.MapGet("open-api-spec/as-html/{id}",EndpointExtensions.OpenApiSpecHtmlGetByIdEndpoint).OpenApiSpecHtmlGetByIdEndpointConfiguration();

        app.MapGet("open-api-spec/by-api-version/{api_version_id}", EndpointExtensions.OpenApiSpecGetByApiVersionIdEndpoint).OpenApiSpecGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("open-api-spec/by-api/{api_id}", EndpointExtensions.OpenApiSpecGetByApiIdEndpoint).OpenApiSpecGetByApiIdEndpointConfiguration();

        #endregion

        //File
        #region File

        app.MapGet("file/{id}", EndpointExtensions.FileGetByIdEndpoint).FileGetByIdEndpointConfiguration();

        app.MapGet("file/list/by-api/{api_id}",EndpointExtensions.FilesGetByApiIdEndpoint).FilesGetByApiIdEndpointConfiguration();

        app.MapGet("file/list/by-api-version/{api_version_id}", EndpointExtensions.FilesGetByApiVersionIdEndpoint).FilesGetByApiVersionIdEndpointConfiguration();

        #endregion

        //Guide
        #region Guide

        app.MapGet("guide/{id}", EndpointExtensions.GuideGetByIdEndpoint).GuideGetByIdEndpointConfiguration();

        app.MapGet("guide/list/{api_id}", EndpointExtensions.GuidesGetByApiIdEndpoint).GuidesGetByApiIdEndpointConfiguration();

        #endregion

        //GuideVersion
        #region GuideVersion
        app.MapGet("guide-version/{id}", EndpointExtensions.GuideVersionGetByIdEndpoint).GuideVersionGetByIdEndpointConfiguration();
        
        app.MapGet("guide-version/by-api-version/{api_version_id}", EndpointExtensions.GuideVersionGetByApiVersionIdEndpoint).GuideVersionGetByApiVersionIdEndpointConfiguration();

        app.MapGet("guide-version/latest/{guide_id}", EndpointExtensions.GuideVersionLatestGetByGuideIdEndpoint).GuideVersionLatestGetByGuideIdEndpointConfiguration();

        #endregion
        
        //DocumentStatus
        #region DocumentStatus
        
        app.MapGet("document-status/by-api-version-id/{api_version_id}", EndpointExtensions.DocumentStatusGetByApiVersionIdEndpoint).DocumentStatusGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("document-status/by-api-id/{api_id}", EndpointExtensions.DocumentStatusGetByApiIdEndpoint).DocumentStatusGetByApiIdEndpointConfiguration();
        #endregion 
        

    }
}