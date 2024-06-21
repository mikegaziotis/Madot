using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record ApiGetByIdRequest([FromRoute(Name = "id")] string Id) : IRequest;

public class ApiGetByIdMediator(
    IQueryHandler<ApiGetByIdQuery, Api?> handler,
    IMapper mapper): IMediator<ApiGetByIdRequest,IResult> 
{
    public async Task<IResult> Send(ApiGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();

        var finalResult = mapper.Map<DTOs.Responses.Api>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiGetByIdEndpoint([FromServices] ApiGetByIdMediator mediator, [AsParameters] ApiGetByIdRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder ApiGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces<DTOs.Responses.Api>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an API record by its Id",
                Description = "Returns all the API definitions that the server has",
                OperationId = "ApiGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}