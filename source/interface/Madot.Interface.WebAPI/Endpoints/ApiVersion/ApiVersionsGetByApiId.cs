using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiVersionsGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class ApiVersionsGetByApiIdEndpoint(
    IQueryHandler<ApiVersionGetByApiIdQuery,IEnumerable<ApiVersion>> handler,
    IMapper mapper): IEndpoint<ApiVersionsGetByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ApiVersionsGetByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByApiIdQuery(request.ApiId));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.ApiVersion>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiVersionsGetByApiIdEndpoint endpoint, [AsParameters] ApiVersionsGetByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiVersionsGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.ApiVersion>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get all ApiVersions for an Api_Id",
                Description = "Returns all the API versions for a provided Api_Id",
                OperationId = "ApiVersionsGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}