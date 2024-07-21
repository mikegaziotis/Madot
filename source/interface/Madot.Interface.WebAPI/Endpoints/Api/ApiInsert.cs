using AutoMapper;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiInsertRequest([FromBody] DTOs.Requests.ApiInsertCommand Command) : IRequest;

public class ApiInsertEndpoint(
    ICommandHandler<Core.Application.Operations.Commands.ApiInsertCommand, Api> handler,
    IMapper mapper): IEndpoint<ApiInsertRequest,IResult> 
{
    public async Task<IResult> Handle(ApiInsertRequest request)
    {
        await handler.Handle(new Core.Application.Operations.Commands.ApiInsertCommand
        {
            Id = request.Command.Id,
            DisplayName = request.Command.DisplayName,
            BaseUrlPath = request.Command.BaseUrlPath,
            IsInternal = request.Command.IsInternal,
            IsPreview = request.Command.IsPreview,
            IsHidden = request.Command.IsHidden,
            OrderId = request.Command.OrderId
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] ApiInsertEndpoint endpoint, [AsParameters] ApiInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    
    public static void ApiInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create an API record",
                Description = "Creates an API record for the given specification",
                OperationId = "ApiInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}