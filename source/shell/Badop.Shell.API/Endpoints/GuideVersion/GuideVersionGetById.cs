using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.GuideVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record GuideVersionGetByIdRequest([FromRoute(Name="id")] string Id) : IRequest;

public class GuideVersionGetByIdMediator(
    IQueryHandler<GuideVersionGetbyIdQuery, GuideVersion?> handler) : IMediator<GuideVersionGetByIdRequest,IResult> 
{
    public async Task<IResult> Send(GuideVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionGetbyIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> GuideVersionGetByIdEndpoint(GuideVersionGetByIdMediator mediator, [AsParameters] GuideVersionGetByIdRequest request)
        => await mediator.Send(request);
    
    public static void GuideVersionGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.GuideVersion>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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