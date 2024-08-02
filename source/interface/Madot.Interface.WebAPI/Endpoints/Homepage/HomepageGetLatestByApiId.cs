using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record HomepageGetLatestByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class HomepageGetLatestByApiIdEndpoint(
    IQueryHandler<VersionedDocumentGetLatestByApiIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<HomepageGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(HomepageGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetLatestByApiIdQuery(request.ApiId, VersionedDocumentType.Homepage));
        if(result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Homepage>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] HomepageGetLatestByApiIdEndpoint endpoint, [AsParameters] HomepageGetLatestByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    
    public static void HomepageGetLatestByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Homepage>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Homepage history for an Api Id",
                Description = "Returns the Homepages for a given Api_id",
                OperationId = "HomepageGetLatestByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

