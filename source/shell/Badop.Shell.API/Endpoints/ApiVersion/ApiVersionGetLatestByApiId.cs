using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record ApiVersionGetLatestByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_hidden")] bool IncludeHidden=false):IRequest;

public class ApiVersionGetLatestByApiIdMediator(
    IQueryHandler<ApiVersionGetLatestByApiIdQuery,ApiVersion?> handler,
    IMapper mapper): IMediator<ApiVersionGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Send(ApiVersionGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetLatestByApiIdQuery(request.ApiId, request.IncludeHidden));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<ApiVersion>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiVersionGetLatestByApiIdEndpoint([FromServices] ApiVersionGetLatestByApiIdMediator mediator, [AsParameters] ApiVersionGetLatestByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void ApiVersionGetLatestByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.ApiVersion>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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