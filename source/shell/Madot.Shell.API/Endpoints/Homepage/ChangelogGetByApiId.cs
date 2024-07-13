using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Homepage;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record HomepageGetByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class HomepageGetByApiIdMediator(
    IQueryHandler<VersionedDocumentGetByApiIdQuery, IEnumerable<VersionedDocument>> handler,
    IMapper mapper) : IMediator<HomepageGetByApiIdRequest,IResult>
{
    public async Task<IResult> Send(HomepageGetByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiIdQuery(request.ApiId, VersionedDocumentType.Homepage));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Homepage>).ToList();
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> HomepageGetByApiIdEndpoint([FromServices] HomepageGetByApiIdMediator mediator, [AsParameters] HomepageGetByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void HomepageGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Homepage>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Homepage history for an Api Id",
                Description = "Returns the Homepages for a given Api_id",
                OperationId = "HomepageGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 

