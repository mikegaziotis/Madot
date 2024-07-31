using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideVersionLatestGetByGuideIdRequest([FromRoute(Name="guide_id")] string GuideId) : IRequest;

public class GuideVersionLatestGetByGuideIdEndpoint(
    IQueryHandler<GuideVersionLatestGetByIdQuery, GuideVersion?> handler,
    IMapper mapper) : IEndpoint<GuideVersionLatestGetByGuideIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionLatestGetByGuideIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionLatestGetByIdQuery(request.GuideId));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(mapper.Map<DTOs.Responses.GuideVersion>(result));
    }
    
    public static async Task<IResult> Send(GuideVersionLatestGetByGuideIdEndpoint endpoint, [AsParameters] GuideVersionLatestGetByGuideIdRequest request)
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void GuideVersionLatestGetByGuideIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.GuideVersion>()
            .Produces(StatusCodes.Status404NotFound)
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