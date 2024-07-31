using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiGetAllRequest([FromQuery(Name="visible_only")] bool VisibleOnly=false):IRequest;


public class ApiGetAllEndpoint(
    IQueryHandler<ApiGetAllQuery, IEnumerable<Api>> handler,
    IMapper mapper): IEndpoint<ApiGetAllRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetAllRequest request)
    {
        var result = await handler.Handle(new ApiGetAllQuery(request.VisibleOnly));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.Api>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ApiGetAllEndpoint endpoint, [AsParameters] ApiGetAllRequest request) 
        => await endpoint.Handle(request);
}
public static partial class EndpointExtensions
{
    
    public static void ApiGetAllEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.Api>>()
            .Produces(StatusCodes.Status404NotFound)
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