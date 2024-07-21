using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiVersionGetByIdRequest([FromRoute(Name="id")] string Id): IRequest;

public class ApiVersionGetByIdEndpoint(
    IQueryHandler<ApiVersionGetByIdQuery, ApiVersion?> handler,
    IMapper mapper) : IEndpoint<ApiVersionGetByIdRequest, IResult>
{
    public async Task<IResult> Handle(ApiVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.ApiVersion>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiVersionGetByIdEndpoint endpoint, [AsParameters] ApiVersionGetByIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    
    
    public static void ApiVersionGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.ApiVersion>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an ApiVersion",
                Description = "Retrieve a specific ApiVersion by its Id",
                OperationId = "ApiVersionGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}