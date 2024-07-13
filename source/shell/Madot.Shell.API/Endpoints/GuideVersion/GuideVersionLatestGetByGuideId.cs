using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.GuideVersion;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record GuideVersionLatestGetByGuideIdRequest([FromRoute(Name="guide_id")] string GuideId) : IRequest;

public class GuideVersionLatestGetByGuideIdMediator(
    IQueryHandler<GuideVersionLatestGetByIdQuery, GuideVersion?> handler,
    IMapper mapper) : IMediator<GuideVersionLatestGetByGuideIdRequest,IResult> 
{
    public async Task<IResult> Send(GuideVersionLatestGetByGuideIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionLatestGetByIdQuery(request.GuideId));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(mapper.Map<DTOs.Responses.GuideVersion>(result));
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> GuideVersionLatestGetByGuideIdEndpoint(GuideVersionLatestGetByGuideIdMediator mediator, [AsParameters] GuideVersionLatestGetByGuideIdRequest request)
        => await mediator.Send(request);
    
    public static void GuideVersionLatestGetByGuideIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.GuideVersion>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(GuideVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the latest Guide Version for a Guide",
                Description = "Returns a Guide Version record given the a Guide_Id",
                OperationId = "GuideVersionLatestGetByGuideId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}