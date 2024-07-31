using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideVersionGetByIdRequest([FromRoute(Name="id")] string Id) : IRequest;

public class GuideVersionGetByIdEndpoint(
    IQueryHandler<GuideVersionGetbyIdQuery, GuideVersion?> handler) : IEndpoint<GuideVersionGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionGetbyIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
    
    public static async Task<IResult> Send(GuideVersionGetByIdEndpoint endpoint, [AsParameters] GuideVersionGetByIdRequest request)
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void GuideVersionGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.GuideVersion>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(GuideVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a Guide Version by its unique id",
                Description = "Returns a Guide Version record given the Id",
                OperationId = "GuideVersionGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}