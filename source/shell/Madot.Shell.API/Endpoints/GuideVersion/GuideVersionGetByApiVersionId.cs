using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.GuideVersion;
using Madot.Shell.API.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record GuideVersionGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId) : IRequest;

public class GuideVersionGetByApiVersionIdMediator(
    IQueryHandler<GuideVersionGetByApiVersionIdQuery, IEnumerable<Madot.Core.Domain.OtherTypes.ApiVersionGuide>> handler,
    IMapper mapper) : IMediator<GuideVersionGetByApiVersionIdRequest,IResult> 
{
    public async Task<IResult> Send(GuideVersionGetByApiVersionIdRequest request)
    {
        var results = await handler.Handle(new GuideVersionGetByApiVersionIdQuery(request.ApiVersionId));
        var finalResults = results.Select(mapper.Map<ApiVersionGuide>).ToList();
        return Results.Ok(finalResults);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> GuideVersionGetByApiVersionIdEndpoint(GuideVersionGetByApiVersionIdMediator mediator, [AsParameters] GuideVersionGetByApiVersionIdRequest request)
        => await mediator.Send(request);
    
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