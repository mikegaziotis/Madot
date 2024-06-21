using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record ChangelogGetByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class ChangelogGetByApiIdMediator(
    IQueryHandler<VersionedDocumentGetByApiIdQuery, IEnumerable<VersionedDocument>> handler,
    IMapper mapper) : IMediator<ChangelogGetByApiIdRequest,IResult>
{
    public async Task<IResult> Send(ChangelogGetByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiIdQuery(request.ApiId, VersionedDocumentType.Changelog));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Changelog>).ToList();
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ChangelogGetByApiIdEndpoint([FromServices] ChangelogGetByApiIdMediator mediator, [AsParameters] ChangelogGetByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void ChangelogGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Changelog>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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

