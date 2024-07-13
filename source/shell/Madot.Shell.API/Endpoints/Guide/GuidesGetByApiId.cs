using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Guide;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record GuidesGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_deleted")] bool IncludeDeleted=false):IRequest;

public class GuidesGetByApiIdMediator(
    IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Guide>> handler):IMediator<GuidesGetByApiIdRequest,IResult>
{
    public async Task<IResult> Send(GuidesGetByApiIdRequest request)
    {
        var result = await handler.Handle(new GuideGetAllByApiIdQuery(request.ApiId, request.IncludeDeleted));
        return Results.Ok(result);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> GuidesGetByApiIdEndpoint(GuidesGetByApiIdMediator mediator, [AsParameters] GuidesGetByApiIdRequest request)
        => await mediator.Send(request);
    
    public static void GuidesGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Guide>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(GuideTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the list of Guides for an Api",
                Description = "Returns a Guide list for a given Api_Id",
                OperationId = "GuidesGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}