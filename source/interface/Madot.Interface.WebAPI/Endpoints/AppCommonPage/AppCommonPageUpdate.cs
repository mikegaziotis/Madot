using AutoMapper;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record AppCommonPageUpdateRequest([FromBody] DTOs.Requests.AppCommonPageUpdateCommand Command) : IRequest;

public class AppCommonPageUpdateEndpoint(
    ICommandHandler<AppCommonPageUpdateCommand, AppCommonPage> handler,
    IMapper mapper): IEndpoint<AppCommonPageUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(AppCommonPageUpdateRequest request)
    {
        await handler.Handle(new AppCommonPageUpdateCommand
        {
            Id = request.Command.Id,
            Data = request.Command.Data,
            OrderId = request.Command.OrderId,
            IsDeleted = request.Command.IsDeleted
        });
        
        return Results.Ok();
    }
    public static async Task<IResult> Send([FromServices] AppCommonPageUpdateEndpoint endpoint, [AsParameters] AppCommonPageUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void AppCommonPageUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update an AppCommonPage record",
                Description = "Updates an AppCommonPage record for the given specification",
                OperationId = "AppCommonPageUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}