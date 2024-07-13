using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.AppCommonPage;
using Madot.Core.Domain.Models;
using Madot.Shell.API.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Api = Madot.Core.Domain.Models.Api;

namespace Madot.Shell.API.Endpoints;

public record AppCommonPagesGetAllRequest([FromQuery(Name="include_deleted")] bool IncludeDeleted=false):IRequest;


public class AppCommonPagesGetAllMediator(
    IQueryHandler<AppCommonPagesGetAllQuery, IEnumerable<AppCommonPage>> handler,
    IMapper mapper): IMediator<AppCommonPagesGetAllRequest,IResult> 
{
    public async Task<IResult> Send(AppCommonPagesGetAllRequest request)
    {
        var result = await handler.Handle(new AppCommonPagesGetAllQuery(request.IncludeDeleted));
        var finalResult = result.Select(mapper.Map<AppCommonPageLite>).ToList();
        return Results.Ok(finalResult);
    }
}
public static partial class EndpointExtensions
{
    public static async Task<IResult> AppCommonPagesGetAllEndpoint([FromServices] AppCommonPagesGetAllMediator mediator, [AsParameters] AppCommonPagesGetAllRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder AppCommonPagesGetAllEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces<IEnumerable<AppCommonPageLite>>()
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get all the static pages for the app",
                Description = "Returns all the app static pages",
                OperationId = "AppCommonPagesGetAll" ,
            })
            //add auth
            .AllowAnonymous();
    }
}