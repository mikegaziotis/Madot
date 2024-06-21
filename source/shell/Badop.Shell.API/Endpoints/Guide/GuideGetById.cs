using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Guide;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record GuideGetByIdRequest([FromRoute(Name="id")] string Id) : IRequest;

public class GuideGetByIdMediator(
    IQueryHandler<GuideGetByIdQuery,Guide?> handler,
    IMapper mapper):IMediator<GuideGetByIdRequest,IResult> 
{
    public async Task<IResult> Send(GuideGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Guide>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> GuideGetByIdEndpoint(GuideGetByIdMediator mediator, [AsParameters] GuideGetByIdRequest request)
        => await mediator.Send(request);
    
    public static void GuideGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Guide>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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