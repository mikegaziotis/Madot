using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiVersionGetLatestByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_hidden")] bool IncludeHidden=false):IRequest;

public class ApiVersionGetLatestByApiIdEndpoint(
    IQueryHandler<ApiVersionGetLatestByApiIdQuery,ApiVersion?> handler,
    IMapper mapper): IEndpoint<ApiVersionGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ApiVersionGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetLatestByApiIdQuery(request.ApiId, request.IncludeHidden));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<ApiVersion>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiVersionGetLatestByApiIdEndpoint endpoint, [AsParameters] ApiVersionGetLatestByApiIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    
    public static void ApiVersionGetLatestByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.ApiVersion>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ApiVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the latest ApiVersion for a given Api_Id",
                Description = "Retrieve a specific ApiVersion by its Id",
                OperationId = "ApiVersionGetLatestByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}