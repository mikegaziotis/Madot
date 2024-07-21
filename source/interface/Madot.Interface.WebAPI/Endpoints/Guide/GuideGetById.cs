using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideGetByIdRequest([FromRoute(Name="id")] string Id) : IRequest;

public class GuideGetByIdEndpoint(
    IQueryHandler<GuideGetByIdQuery,Guide?> handler,
    IMapper mapper):IEndpoint<GuideGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Guide>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send(GuideGetByIdEndpoint endpoint, [AsParameters] GuideGetByIdRequest request)
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void GuideGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Guide>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(GuideTag)
            .WithOpenApi(op => new(op)
            {
                Summary = "Get a Guide by its unique id",
                Description = "Returns a Guide record given the Guide Id",
                OperationId = "GuideGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}