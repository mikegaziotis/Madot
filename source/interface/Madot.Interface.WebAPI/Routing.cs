using Madot.Interface.WebAPI.Endpoints;


public static class Routing
{
    public static void AddRouting(this WebApplication app)
    {
        //Api
        #region Api

        app.MapGet("api/{id}", ApiGetByIdEndpoint.Send).ApiGetByIdEndpointConfiguration();

        app.MapGet("api/list", ApiGetAllEndpoint.Send).ApiGetAllEndpointConfiguration();
        
        app.MapGet("api/search-by-name", ApiSearchByNameEndpoint.Send).ApiSearchByNameEndpointConfiguration();

        app.MapPost("api", ApiInsertEndpoint.Send).ApiInsertEndpointConfiguration();
        
        app.MapPut("api", ApiUpdateEndpoint.Send).ApiUpdateEndpointConfiguration();
        
        #endregion
        
        //Api Version
        #region Api Version

        app.MapGet("api-version/{id}", ApiVersionGetByIdEndpoint.Send).ApiVersionGetByIdEndpointConfiguration();

        app.MapGet("api-version/latest/{api_id}", ApiVersionGetLatestByApiIdEndpoint.Send).ApiVersionGetLatestByApiIdEndpointConfiguration(); 
        
        app.MapGet("api-version/list/{api_id}", ApiVersionsGetByApiIdEndpoint.Send).ApiVersionsGetByApiIdEndpointConfiguration();
        
        app.MapPost("api-version", ApiVersionInsertEndpoint.Send).ApiVersionInsertEndpointConfiguration();
        
        app.MapPut("api-version", ApiVersionUpdateEndpoint.Send).ApiVersionUpdateEndpointConfiguration();
        
        #endregion
        
        //App Common Page
        #region App Static Page

        app.MapGet("app-common-page/{id:int}", AppCommonPageGetByIdEndpoint.Send).AppCommonPageGetByIdEndpointConfiguration();
        
        app.MapGet("app-common-page/list", AppCommonPagesGetAllEndpoint.Send).AppCommonPagesGetAllEndpointConfiguration();
        
        app.MapPost("app-common-page", AppCommonPageInsertEndpoint.Send).AppCommonPageInsertEndpointConfiguration();
        
        app.MapPut("app-common-page", AppCommonPageUpdateEndpoint.Send).AppCommonPageUpdateEndpointConfiguration();
        
        #endregion
        
        //Changelog
        #region Changelog

        app.MapGet("changelog/{id}",ChangelogGetByIdEndpoint.Send).ChangelogGetByIdEndpointConfiguration();

        app.MapGet("changelog/by-api-version/{api_version_id}", ChangelogGetByApiVersionIdEndpoint.Send).ChangelogGetByApiVersionIdEndpointConfiguration();

        app.MapGet("changelog/by-api/{api_id}", ChangelogGetByApiIdEndpoint.Send).ChangelogGetByApiIdEndpointConfiguration();
        
        app.MapGet("changelog/latest/{api_id}", ChangelogGetLatestByApiIdEndpoint.Send).ChangelogGetLatestByApiIdEndpointConfiguration();
        
        app.MapPost("changelog",ChangelogInsertEndpoint.Send).ChangelogInsertEndpointConfiguration();
        
        app.MapPut("changelog",ChangelogUpdateEndpoint.Send).ChangelogUpdateEndpointConfiguration();
        #endregion
        
        //Homepage
        #region Homepage

        app.MapGet("homepage/{id}",HomepageGetByIdEndpoint.Send).HomepageGetByIdEndpointConfiguration();

        app.MapGet("homepage/by-api-version/{api_version_id}", HomepageGetByApiVersionIdEndpoint.Send).HomepageGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("homepage/by-api/{api_id}", HomepageGetByApiIdEndpoint.Send).HomepageGetByApiIdEndpointConfiguration();
        
        app.MapGet("homepage/latest/{api_id}", HomepageGetLatestByApiIdEndpoint.Send).HomepageGetLatestByApiIdEndpointConfiguration();
        
        app.MapPost("homepage",HomepageInsertEndpoint.Send).HomepageInsertEndpointConfiguration();
        
        app.MapPut("homepage",HomepageUpdateEndpoint.Send).HomepageUpdateEndpointConfiguration();

        #endregion

        //Open Api Specification
        #region Open Api Specification

        app.MapGet("open-api-spec/{id}",OpenApiSpecGetByIdEndpoint.Send).OpenApiSpecGetByIdEndpointConfiguration();
        
        app.MapGet("open-api-spec/as-html/{id}",OpenApiSpecHtmlGetByIdEndpoint.Send).OpenApiSpecHtmlGetByIdEndpointConfiguration();

        app.MapGet("open-api-spec/by-api-version/{api_version_id}", OpenApiSpecGetByApiVersionIdEndpoint.Send).OpenApiSpecGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("open-api-spec/by-api/{api_id}", OpenApiSpecGetByApiIdEndpoint.Send).OpenApiSpecGetByApiIdEndpointConfiguration();
        
        app.MapGet("open-api-spec/latest/{api_id}", OpenApiSpecGetLatestByApiIdEndpoint.Send).OpenApiSpecGetLatestByApiIdEndpointConfiguration();
        
        app.MapPost("open-api-spec",OpenApiSpecInsertEndpoint.Send).OpenApiSpecInsertEndpointConfiguration();
        
        app.MapPut("open-api-spec",OpenApiSpecUpdateEndpoint.Send).OpenApiSpecUpdateEndpointConfiguration();

        #endregion

        //File
        #region File

        app.MapGet("file/{id}", FileGetByIdEndpoint.Send).FileGetByIdEndpointConfiguration();

        app.MapGet("file/list/by-api/{api_id}",FilesGetByApiIdEndpoint.Send).FilesGetByApiIdEndpointConfiguration();

        app.MapGet("file/list/by-api-version/{api_version_id}", FilesGetByApiVersionIdEndpoint.Send).FilesGetByApiVersionIdEndpointConfiguration();

        app.MapPost("file", FileInsertEndpoint.Send).FileInsertEndpointConfiguration();
        
        app.MapPut("file", FileUpdateEndpoint.Send).FileUpdateEndpointConfiguration();
        
        #endregion

        //Guide
        #region Guide

        app.MapGet("guide/{id}", GuideGetByIdEndpoint.Send).GuideGetByIdEndpointConfiguration();

        app.MapGet("guide/list/{api_id}", GuidesGetByApiIdEndpoint.Send).GuidesGetByApiIdEndpointConfiguration();
        
        app.MapPost("guide", GuideInsertEndpoint.Send).GuideInsertEndpointConfiguration();
        
        app.MapPut("guide",GuideUpdateEndpoint.Send).GuideUpdateEndpointConfiguration();

        #endregion

        //GuideVersion
        #region GuideVersion
        app.MapGet("guide-version/{id}", GuideVersionGetByIdEndpoint.Send).GuideVersionGetByIdEndpointConfiguration();
        
        app.MapGet("guide-version/by-api-version/{api_version_id}", GuideVersionGetByApiVersionIdEndpoint.Send).GuideVersionGetByApiVersionIdEndpointConfiguration();

        app.MapGet("guide-version/latest/{guide_id}", GuideVersionLatestGetByGuideIdEndpoint.Send).GuideVersionLatestGetByGuideIdEndpointConfiguration();
        
        app.MapPost("guide-version",GuideVersionInsertEndpoint.Send).GuideVersionInsertEndpointConfiguration();
        
        app.MapPut("guide-version",GuideVersionUpdateEndpoint.Send).GuideVersionUpdateEndpointConfiguration();

        #endregion
        
        //DocumentStatus
        #region DocumentStatus
        
        app.MapGet("document-status/by-api-version-id/{api_version_id}", DocumentStatusGetByApiVersionIdEndpoint.Send).DocumentStatusGetByApiVersionIdEndpointConfiguration();
        
        app.MapGet("document-status/by-api-id/{api_id}", DocumentStatusGetByApiIdEndpoint.Send).DocumentStatusGetByApiIdEndpointConfiguration();
        
        #endregion 
        

    }
}