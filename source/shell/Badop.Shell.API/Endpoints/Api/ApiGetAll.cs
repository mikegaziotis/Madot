using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Endpoints;

public record ApiGetAllRequest([FromQuery(Name="visible_only")] bool VisibleOnly=false):IRequest;


public class ApiGetAllMediator(
    IQueryHandler<ApiGetAllQuery, IEnumerable<Api>> handler,
    IMapper mapper): IMediator<ApiGetAllRequest,IResult> 
{
    public async Task<IResult> Send(ApiGetAllRequest request)
    {
        var result = await handler.Handle(new ApiGetAllQuery(request.VisibleOnly));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Api>).ToList();
        return Results.Ok(finalResult);
    }
}
public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiGetAllEndpoint([FromServices] ApiGetAllMediator mediator, [AsParameters] ApiGetAllRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder ApiGetAllEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Api>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get all API records",
                Description = "Returns all the API definitions that the server has",
                OperationId = "ApiGetAll" ,
            })
            //add auth
            .AllowAnonymous();
    }
}