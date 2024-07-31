using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record OpenApiSpecGetByApiVersionIdRequest([FromRoute(Name = "api_version_id")]string ApiVersionId): IRequest;

public class OpenApiSpecGetByApiVersionIdEndpoint(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<OpenApiSpecGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Handle(OpenApiSpecGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.OpenApiSpec>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] OpenApiSpecGetByApiVersionIdEndpoint endpoint, [AsParameters] OpenApiSpecGetByApiVersionIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void OpenApiSpecGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.OpenApiSpec>()
            .Produces(StatusCodes.Status404NotFound)
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