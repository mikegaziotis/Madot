using AutoMapper;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Application.Operations.Commands.Api;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record ApiInsertRequest([FromBody] DTOs.Requests.ApiInsertCommand Command) : IRequest;

public class ApiInsertMediator(
    ICommandHandler<ApiInsertCommand, Api> handler,
    IMapper mapper): IMediator<ApiInsertRequest,IResult> 
{
    public async Task<IResult> Send(ApiInsertRequest request)
    {
        await handler.Handle(new ApiInsertCommand
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
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ApiInsertEndpoint([FromServices] ApiInsertMediator mediator, [AsParameters] ApiInsertRequest request) 
        => await mediator.Send(request);
    public static RouteHandlerBuilder ApiInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        return builder
            //add open api description
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create an API record",
                Description = "Cretes an API record for the given specification",
                OperationId = "ApiInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}