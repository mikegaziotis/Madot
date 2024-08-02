using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogGetLatestByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class ChangelogGetLatestByApiIdEndpoint(
    IQueryHandler<VersionedDocumentGetLatestByApiIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<ChangelogGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ChangelogGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetLatestByApiIdQuery(request.ApiId, VersionedDocumentType.Changelog));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Changelog>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogGetLatestByApiIdEndpoint endpoint, [AsParameters] ChangelogGetLatestByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    
    public static void ChangelogGetLatestByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Changelog>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Changelog history for an Api Id",
                Description = "Returns the Changelogs for a given Api_id",
                OperationId = "ChangelogGetLatestByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

