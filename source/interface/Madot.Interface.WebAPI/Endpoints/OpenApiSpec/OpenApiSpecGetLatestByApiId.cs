using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record OpenApiSpecGetLatestByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class OpenApiSpecGetLatestByApiIdEndpoint(
    IQueryHandler<VersionedDocumentGetLatestByApiIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<OpenApiSpecGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(OpenApiSpecGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetLatestByApiIdQuery(request.ApiId, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.OpenApiSpec>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] OpenApiSpecGetLatestByApiIdEndpoint endpoint, [AsParameters] OpenApiSpecGetLatestByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    
    public static void OpenApiSpecGetLatestByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.OpenApiSpec>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the OpenApiSpec history for an Api Id",
                Description = "Returns the OpenApiSpecs for a given Api_id",
                OperationId = "OpenApiSpecGetLatestByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

