using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Badop.Core.Domain.Models.VersionedDocument;

namespace Badop.Shell.API.Endpoints;

public record OpenApiSpecGetByApiVersionIdRequest([FromRoute(Name = "api_version_id")]string ApiVersionId): IRequest;

public class OpenApiSpecGetByApiVersionIdMediator(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IMediator<OpenApiSpecGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Send(OpenApiSpecGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.OpenApiSpec>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> OpenApiSpecGetByApiVersionIdEndpoint([FromServices] OpenApiSpecGetByApiVersionIdMediator mediator, [AsParameters] OpenApiSpecGetByApiVersionIdRequest request) 
        => await mediator.Send(request);
    
    public static void OpenApiSpecGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.OpenApiSpec>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the OpenApiSpec for an Api Version",
                Description = "Returns the OpenApiSpec for a give Api_Version_id",
                OperationId = "OpenApiSpecGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 