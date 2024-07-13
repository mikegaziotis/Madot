using System.Text.Encodings.Web;
using System.Text.Json;
using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Homepage;
using Madot.Core.Domain.Enums;
using Madot.Shell.API.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Shell.API.Endpoints;

public record OpenApiSpecHtmlGetByIdRequest([FromRoute(Name = "id")]string Id):IRequest;

public class OpenApiSpecHtmlGetByIdMediator(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IMediator<OpenApiSpecHtmlGetByIdRequest,IResult>
{
    private const string BaseHtml =
        "<html>\n  <head>\n    <title>Scalar API Reference</title>\n    <meta charset=\"utf-8\" />\n    <meta\n      name=\"viewport\"\n      content=\"width=device-width, initial-scale=1\" />\n  </head>\n  <body>\n    \n<script\n  id=\"api-reference\"\n  type=\"application/json\"\n  data-proxy-url=\"https://proxy.scalar.com\">\n{OpenApiSpec}\n</script>\n<script>\n      var configuration = {\n        theme: 'default',\n\t\tdarkMode: false\n      }\n\n      document.getElementById('api-reference').dataset.configuration =\n        JSON.stringify(configuration)\n    </script>\n    <script src=\"https://cdn.jsdelivr.net/npm/@scalar/api-reference\"></script>\n  </body>\n</html>\n";
    public async Task<IResult> Send(OpenApiSpecHtmlGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var data = (mapper.Map<DTOs.Responses.OpenApiSpec>(result)).Data;
        var htmlResult = BaseHtml.Replace("{OpenApiSpec}", data);
        return Results.Extensions.Html(htmlResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> OpenApiSpecHtmlGetByIdEndpoint([FromServices] OpenApiSpecHtmlGetByIdMediator mediator, [AsParameters] OpenApiSpecHtmlGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void OpenApiSpecHtmlGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an OpenApiSpec by its Id, in HTML format",
                Description = "Returns the OpenApiSpec's HTML for a given Id",
                OperationId = "OpenApiSpecHtmlGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

