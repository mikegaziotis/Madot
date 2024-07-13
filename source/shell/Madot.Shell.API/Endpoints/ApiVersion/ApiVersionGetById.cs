using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.ApiVersion;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record ApiVersionGetByIdRequest([FromRoute(Name="id")] string Id): IRequest;

public class ApiVersionGetByIdMediator(
    IQueryHandler<ApiVersionGetByIdQuery, ApiVersion?> handler,
    IMapper mapper) : IMediator<ApiVersionGetByIdRequest, IResult>
{
    public async Task<IResult> Send(ApiVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.ApiVersion>(result);
        return Results.Ok(finalResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiVersionGetByIdEndpoint([FromServices] ApiVersionGetByIdMediator mediator, [AsParameters] ApiVersionGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void ApiVersionGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.ApiVersion>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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