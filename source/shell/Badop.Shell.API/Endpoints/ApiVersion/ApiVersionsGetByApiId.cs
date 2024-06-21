using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record ApiVersionsGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class ApiVersionsGetByApiIdMediator(
    IQueryHandler<ApiVersionGetByApiIdQuery,IEnumerable<ApiVersion>> handler,
    IMapper mapper): IMediator<ApiVersionsGetByApiIdRequest,IResult>
{
    public async Task<IResult> Send(ApiVersionsGetByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByApiIdQuery(request.ApiId));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.ApiVersion>).ToList();
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiVersionsGetByApiIdEndpoint([FromServices] ApiVersionsGetByApiIdMediator mediator, [AsParameters] ApiVersionsGetByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void ApiVersionsGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.ApiVersion>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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