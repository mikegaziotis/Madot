using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogGetByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class ChangelogGetByApiIdEndpoint(
    IQueryHandler<VersionedDocumentGetByApiIdQuery, IEnumerable<VersionedDocument>> handler,
    IMapper mapper) : IEndpoint<ChangelogGetByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ChangelogGetByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiIdQuery(request.ApiId, VersionedDocumentType.Changelog));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Changelog>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogGetByApiIdEndpoint endpoint, [AsParameters] ChangelogGetByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    
    public static void ChangelogGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Changelog>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Changelog history for an Api Id",
                Description = "Returns the Changelogs for a given Api_id",
                OperationId = "ChangelogGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

