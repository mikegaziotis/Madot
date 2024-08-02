using AutoMapper;
using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using AppCommonPage = Madot.Core.Domain.Models.AppCommonPage;

namespace Madot.Interface.WebAPI.Endpoints;

public record AppCommonPageInsertRequest([FromBody] DTOs.Requests.AppCommonPageInsertCommand Command) : IRequest;

public class AppCommonPageInsertEndpoint(
    ICommandHandler<AppCommonPageInsertCommand, AppCommonPage, int> handler,
    IMapper mapper): IEndpoint<AppCommonPageInsertRequest,IResult> 
{
    public async Task<IResult> Handle(AppCommonPageInsertRequest request)
    {
        var appCommonPageId = await handler.Handle(new AppCommonPageInsertCommand
        {
            Title = request.Command.Title,
            Data = request.Command.Data,
            OrderId = request.Command.OrderId
        });
        
        return Results.Ok(new IntIdCreated(appCommonPageId));
    }
    
    public static async Task<IResult> Send([FromServices] AppCommonPageInsertEndpoint endpoint, [AsParameters] AppCommonPageInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void AppCommonPageInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces<IntIdCreated>()
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(AppCommonPageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create an AppCommonPage record",
                Description = "Creates an AppCommonPage record for the given specification",
                OperationId = "AppCommonPageInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}