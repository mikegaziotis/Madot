using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Api;
using Madot.Core.Application.Operations.Queries.AppCommonPage;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record AppCommonPageGetByIdRequest([FromRoute(Name = "id")] int Id) : IRequest;

public class AppCommonPageGetByIdMediator(
    IQueryHandler<AppCommonPageGetByIdQuery, AppCommonPage?> handler,
    IMapper mapper): IMediator<AppCommonPageGetByIdRequest,IResult> 
{
    public async Task<IResult> Send(AppCommonPageGetByIdRequest request)
    {
        var result = await handler.Handle(new AppCommonPageGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> AppCommonPageGetByIdEndpoint([FromServices] AppCommonPageGetByIdMediator mediator, [AsParameters] AppCommonPageGetByIdRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder AppCommonPageGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces<AppCommonPage>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an app static page by its Id",
                Description = "Returns the app static page for the given Id",
                OperationId = "AppCommonPageGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}