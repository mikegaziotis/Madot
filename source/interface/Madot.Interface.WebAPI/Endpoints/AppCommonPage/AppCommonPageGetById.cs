using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record AppCommonPageGetByIdRequest([FromRoute(Name = "id")] int Id) : IRequest;

public class AppCommonPageGetByIdEndpoint(
    IQueryHandler<AppCommonPageGetByIdQuery, AppCommonPage?> handler,
    IMapper mapper): IEndpoint<AppCommonPageGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(AppCommonPageGetByIdRequest request)
    {
        var result = await handler.Handle(new AppCommonPageGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();

        return Results.Ok(mapper.Map<DTOs.Responses.AppCommonPage>(result));
    }
    
    public static async Task<IResult> Send([FromServices] AppCommonPageGetByIdEndpoint endpoint, [AsParameters] AppCommonPageGetByIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    public static void AppCommonPageGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.AppCommonPage>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an AppCommonPage by its Id",
                Description = "Returns the AppCommonPage for the given Id",
                OperationId = "AppCommonPageGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}