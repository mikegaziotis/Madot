using AutoMapper;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;


public record ApiUpdateRequest([FromBody] DTOs.Requests.ApiUpdateCommand Command) : IRequest;

public class ApiUpdateEndpoint(
    ICommandHandler<ApiUpdateCommand, Api> handler,
    IMapper mapper): IEndpoint<ApiUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(ApiUpdateRequest request)
    {
        await handler.Handle(new ApiUpdateCommand
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
    
    public static async Task<IResult> Send([FromServices] ApiUpdateEndpoint endpoint, [AsParameters] ApiUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ApiTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update an API record",
                Description = "Updates an API record so it matches the given specification",
                OperationId = "ApiUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}