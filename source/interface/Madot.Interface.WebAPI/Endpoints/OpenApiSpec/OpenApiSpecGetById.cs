using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record OpenApiSpecGetByIdRequest([FromRoute(Name = "id")]string Id):IRequest;

public class OpenApiSpecGetByIdEndpoint(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IEndpoint<OpenApiSpecGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(OpenApiSpecGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.OpenApiSpec>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] OpenApiSpecGetByIdEndpoint endpoint, [AsParameters] OpenApiSpecGetByIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void OpenApiSpecGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.OpenApiSpec>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an OpenApiSpec by its Id",
                Description = "Returns the OpenApiSpec for a given Id",
                OperationId = "OpenApiSpecGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 