using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Microsoft.AspNetCore.Mvc;
using Madot.Core.Domain.Models;

namespace Madot.Interface.WebAPI.Endpoints;

public record AppCommonPagesGetAllRequest([FromQuery(Name="include_deleted")] bool IncludeDeleted=false):IRequest;


public class AppCommonPagesGetAllEndpoint(
    IQueryHandler<AppCommonPagesGetAllQuery, IEnumerable<AppCommonPage>> handler,
    IMapper mapper): IEndpoint<AppCommonPagesGetAllRequest,IResult> 
{
    public async Task<IResult> Handle(AppCommonPagesGetAllRequest request)
    {
        var result = await handler.Handle(new AppCommonPagesGetAllQuery(request.IncludeDeleted));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.AppCommonPageLite>).ToList();
        return Results.Ok(finalResult);
    }

    public static async Task<IResult> Send([FromServices] AppCommonPagesGetAllEndpoint endpoint,
        [AsParameters] AppCommonPagesGetAllRequest request)
        => await endpoint.Handle(request);
}
public static partial class EndpointExtensions
{
    
    public static void AppCommonPagesGetAllEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.AppCommonPageLite>>()
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get all the AppCommonPages for the app",
                Description = "Returns all the AppCommonPage records",
                OperationId = "AppCommonPagesGetAll" ,
            })
            //add auth
            .AllowAnonymous();
    }
}