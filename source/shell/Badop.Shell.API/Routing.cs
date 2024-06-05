using Badop.Core.Domain.Enums;
using Badop.Shell.API.Handlers;
using Badop.Shell.API.Requests;
using Microsoft.AspNetCore.Mvc;


public static class Routing
{
    public static void AddRouting(this WebApplication app)
    {
        //Api
        #region Api
        app.MapGet("api/{api_id}", async (ApiGetByIdHandler handler, [AsParameters] ApiGetByIdRequest request) => await handler.Handle(request))
            .WithTags("Api");

        app.MapGet("api/get-all", async (ApiGetAllHandler handler, [AsParameters] ApiGetAllRequest request) => await handler.Handle(request))
            .WithTags("Api");
        //app.MapPost("api", async (ApiPostHandler handler, [AsParameters] ApiPostRequest request) => await handler.Handle(request)).WithTags("Api");
        #endregion
        
        //Api Version
        #region Api Version

        app.MapGet("api-version/{api_version_id}", async (ApiVersionGetByIdHandler handler, [AsParameters] ApiVersionGetByIdRequest request) => await handler.Handle(request))
            .WithTags(("Api Version"));
        
        app.MapGet("api-version/all-history/{api_id}", async (ApiVersionGetByApiIdHandler handler, [AsParameters] ApiVersionGetByApiIdRequest request) => await handler.Handle(request))
            .WithTags(("Api Version"));
        
        app.MapGet("api-version/latest/{api_id}", async (ApiVersionGetLatestByApiIdHandler handler, [AsParameters] ApiVersionGetLatestByApiIdRequest request) => await handler.Handle(request))
            .WithTags(("Api Version"));
        
        app.MapGet("api-version/short-history/{api_id}", async (ApiVersionShortGetByApiIdHandler handler, [AsParameters] ApiVersionShortGetByApiIdRequest request) => await handler.Handle(request))
            .WithTags(("Api Version"));
        
        #endregion
        
        //Changelog
        #region Changelog

        app.MapGet("changelog/{log_id}",
            async (VersionedDocumentDataGetByIdHandler handler, [FromRoute(Name="log_id")] string id) =>
            await handler.Handle(new VersionedDocumentDataGetByIdRequest(id, VersionedDocumentType.Changelog)))
        .WithTags("Changelog");

        app.MapGet("changelog/for-api-version/{api_version_id}", 
                async (VersionedDocumentGetByApiVersionIdHandler handler, [FromRoute(Name="api_version_id")] string apiVersionId) => 
                await handler.Handle(new VersionedDocumentGetByApiVersionIdRequest(apiVersionId, VersionedDocumentType.Changelog)))
        .WithTags("Changelog");

        app.MapGet("changelog/change-history/{api_id}", 
                async (VersionedDocumentShortGetByApiIdHandler handler, [FromRoute(Name="api_id")] string apiId) => 
                await handler.Handle(new VersionedDocumentShortGetByApiIdRequest(apiId, VersionedDocumentType.Changelog)))
            .WithTags("Changelog");
        
        #endregion
        
        //Homepage
        #region Homepage

        app.MapGet("homepage/{page_id}",
                async (VersionedDocumentDataGetByIdHandler handler, [FromRoute(Name="page_id")] string id) =>
                await handler.Handle(new VersionedDocumentDataGetByIdRequest(id, VersionedDocumentType.Homepage)))
            .WithTags("Homepage");

        app.MapGet("homepage/for-api-version/{api_version_id}", 
                async (VersionedDocumentGetByApiVersionIdHandler handler, [FromRoute(Name="api_version_id")] string apiVersionId) => 
                await handler.Handle(new VersionedDocumentGetByApiVersionIdRequest(apiVersionId, VersionedDocumentType.Homepage)))
            .WithTags("Homepage");

        app.MapGet("homepage/change-history/{api_id}", 
                async (VersionedDocumentShortGetByApiIdHandler handler, [FromRoute(Name="api_id")] string apiId) => 
                await handler.Handle(new VersionedDocumentShortGetByApiIdRequest(apiId, VersionedDocumentType.Homepage)))
            .WithTags("Homepage");
        
        #endregion
        
        //Open Api Specification
        #region Open Api Specification

        app.MapGet("open-api-spec/{oas_id}",
                async (VersionedDocumentDataGetByIdHandler handler, [FromRoute(Name="oas_id")] string id) =>
                await handler.Handle(new VersionedDocumentDataGetByIdRequest(id, VersionedDocumentType.OpenApiSpec)))
            .WithTags("Open Api Specification");

        app.MapGet("open-api-spec/for-api-version/{api_version_id}", 
                async (VersionedDocumentGetByApiVersionIdHandler handler, [FromRoute(Name="api_version_id")] string apiVersionId) => 
                await handler.Handle(new VersionedDocumentGetByApiVersionIdRequest(apiVersionId, VersionedDocumentType.OpenApiSpec)))
            .WithTags("Open Api Specification");

        app.MapGet("open-api-spec/change-history/{api_id}", 
                async (VersionedDocumentShortGetByApiIdHandler handler, [FromRoute(Name="api_id")] string apiId) => 
                await handler.Handle(new VersionedDocumentShortGetByApiIdRequest(apiId, VersionedDocumentType.OpenApiSpec)))
            .WithTags("Open Api Specification");
        
        #endregion
        
        //File
        #region File

        app.MapGet("file/{file_id}",
            async (FileGetByIdHandler handler, [AsParameters] FileGetByIdRequest request) => await handler.Handle(request))
            .WithTags("File");

        app.MapGet("file/for-api/{api_id}",
                async (FileGetByApiIdHandler handler, [AsParameters] FileGetByApiIdRequest request) => await handler.Handle(request))
            .WithTags("File");
        #endregion
        
        //Guide
        #region Guide

        app.MapGet("guide/for-api/{api_id}",
            async (GuideGetByApiIdHandler handler, [AsParameters] GuideGetByApiIdRequest request) => await handler.Handle(request))
            .WithTags("Guide");
        
        app.MapGet("guide/{guide_id}",
                async (GuideGetByIdHandler handler, [AsParameters] GuideGetByIdRequest request) => await handler.Handle(request))
            .WithTags("Guide");

        #endregion

        
        //Guide Version
        #region Guide Version
        
        app.MapGet("guide-version/{guide_version_id}",
                async (GuideVersionGetByIdHandler handler, [AsParameters] GuideVersionGetByIdRequest request) => await handler.Handle(request))
            .WithTags("Guide Version");
        
        app.MapGet("guide-version/for-api-version/{api_version_id}",
                async (GuideVersionGetByApiVersionIdHandler handler, [AsParameters] GuideVersionGetByApiVersionIdRequest request) => await handler.Handle(request))
            .WithTags("Guide Version");
        #endregion

    }
}