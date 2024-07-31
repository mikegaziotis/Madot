using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideVersionGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId) : IRequest;

public class GuideVersionGetByApiVersionIdEndpoint(
    IQueryHandler<GuideVersionGetByApiVersionIdQuery, IEnumerable<Madot.Core.Domain.OtherTypes.ApiVersionGuide>> handler,
    IMapper mapper) : IEndpoint<GuideVersionGetByApiVersionIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionGetByApiVersionIdRequest request)
    {
        var results = await handler.Handle(new GuideVersionGetByApiVersionIdQuery(request.ApiVersionId));
        var finalResults = results.Select(mapper.Map<ApiVersionGuide>).ToList();
        return Results.Ok(finalResults);
    }
    
    public static async Task<IResult> Send(GuideVersionGetByApiVersionIdEndpoint endpoint, [AsParameters] GuideVersionGetByApiVersionIdRequest request)
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void GuideVersionGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<ApiVersionGuide>>()
            .WithTags(GuideVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a list of GuidesVersions for an Api's VersionId",
                Description = "",
                OperationId = "GuideVersionGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}