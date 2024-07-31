using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record HomepageGetByIdRequest([FromRoute(Name="id")]string Id):IRequest;

public class HomepageGetByIdEndpoint(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IEndpoint<HomepageGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(HomepageGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.Homepage));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Homepage>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] HomepageGetByIdEndpoint endpoint, [AsParameters] HomepageGetByIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void HomepageGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Homepage>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a Homepage by its Id",
                Description = "Returns the Homepage for a given Id",
                OperationId = "HomepageGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 