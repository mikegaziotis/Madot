using Badop.Core.Domain.Enums;
using Badop.Shell.API.Handlers;
using Badop.Shell.API.Requests;


public static class Routing
{
    public static void AddRouting(this WebApplication app)
    {
        //api
        #region api
        app.MapGet("api/{apiName}", async (ApiGetHandler handler, [AsParameters] ApiGetRequest request) => await handler.Handle(request)).WithTags("Api");

        app.MapPost("api", async (ApiPostHandler handler, [AsParameters] ApiPostRequest request) => await handler.Handle(request)).WithTags("Api");

        app.MapPatch("api/{apiName}", async (ApiPatchHandler handler, [AsParameters] ApiPatchRequest request) => await handler.Handle(request)).WithTags("Api");
        #endregion
        
        //apiVersion
        #region apiVersion

        app.MapGet("api-version/{id}", () =>
        {
            throw new NotImplementedException();
        }).WithTags("ApiVersion");

        app.MapPost("api-version", () =>
        {
            throw new NotImplementedException();
        }).WithTags("ApiVersion");
        
        app.MapPatch("api-version/{apiKey}", () =>
        {
            throw new NotImplementedException();
        }).WithTags("ApiVersion");
        #endregion
        
        //changeLog
        #region changeLog

        app.MapPost("change-log/{apiKey}", () =>
        {
            throw new NotImplementedException();
        }).WithTags("ChangeLog");

        app.MapGet("change-log/{api_version}", () =>
        {
            throw new NotImplementedException();
        }).WithTags("ChangeLog");

        #endregion
        
        //versionedDocument
        #region versionedDocument
        app.MapGet("versioned-document/{id}", () =>
        {
            throw new NotImplementedException();
        }).WithTags("VersionedDocument");
        app.MapGet("versioned-document/latest", (string api, VersionedDocumentType type, bool? include_preview) =>
        {
            throw new NotImplementedException();
        }).WithTags("VersionedDocument");
        app.MapPost("versioned-document", () =>
        {
            throw new NotImplementedException();
        }).WithTags("VersionedDocument");
        #endregion
    }
}