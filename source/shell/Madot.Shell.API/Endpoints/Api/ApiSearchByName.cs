using AutoMapper;
using Madot.Core.Application.Enums;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Api;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record ApiSearchByNameRequest([FromQuery(Name="search_term")] string SearchTerm, [FromQuery] SearchMethod SearchMethod):IRequest;


public class ApiSearchByNameMediator(
    IQueryHandler<ApiSearchByNameQuery, IEnumerable<Api>> handler, 
    IMapper mapper) : IMediator<ApiSearchByNameRequest, IResult>
{
    public async Task<IResult> Send(ApiSearchByNameRequest request)
    {
        var result = await handler.Handle(new ApiSearchByNameQuery(request.SearchTerm, request.SearchMethod));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Api>).ToList();
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiSearchByNameEndpoint([FromServices] ApiSearchByNameMediator mediator, [AsParameters] ApiSearchByNameRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder ApiSearchByNameEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Api>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Search for API by their Display Name property",
                Description = "Returns all the API definitions that match the search terms provided",
                OperationId = "ApiSearchByName" ,
            })
            //add auth
            .AllowAnonymous();
    }
}