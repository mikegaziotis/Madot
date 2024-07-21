using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiGetByIdRequest([FromRoute(Name = "id")] string Id) : IRequest;

public class ApiGetByIdEndpoint(
    IQueryHandler<ApiGetByIdQuery, Api?> handler,
    IMapper mapper): IEndpoint<ApiGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();

        var finalResult = mapper.Map<DTOs.Responses.Api>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiGetByIdEndpoint endpoint, [AsParameters] ApiGetByIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Api>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an API record by its Id",
                Description = "Returns the API definitions for an Api given an Id",
                OperationId = "ApiGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}