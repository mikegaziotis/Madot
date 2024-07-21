using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuidesGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_deleted")] bool IncludeDeleted=false):IRequest;

public class GuidesGetByApiIdEndpoint(
    IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Guide>> handler):IEndpoint<GuidesGetByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(GuidesGetByApiIdRequest request)
    {
        var result = await handler.Handle(new GuideGetAllByApiIdQuery(request.ApiId, request.IncludeDeleted));
        return Results.Ok(result);
    }

    public static async Task<IResult> Send(GuidesGetByApiIdEndpoint endpoint, [AsParameters] GuidesGetByApiIdRequest request)
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void GuidesGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Guide>>()
            .Produces(StatusCodes.Status404NotFound)
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