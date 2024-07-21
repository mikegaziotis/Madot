using AutoMapper;
using Madot.Core.Application.Enums;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiSearchByNameRequest([FromQuery(Name="search_term")] string SearchTerm, [FromQuery] SearchMethod SearchMethod):IRequest;


public class ApiSearchByNameEndpoint(
    IQueryHandler<ApiSearchByNameQuery, IEnumerable<Api>> handler, 
    IMapper mapper) : IEndpoint<ApiSearchByNameRequest, IResult>
{
    public async Task<IResult> Handle(ApiSearchByNameRequest request)
    {
        var result = await handler.Handle(new ApiSearchByNameQuery(request.SearchTerm, request.SearchMethod));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Api>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiSearchByNameEndpoint endpoint, [AsParameters] ApiSearchByNameRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiSearchByNameEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Api>>()
            .Produces(StatusCodes.Status404NotFound)
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