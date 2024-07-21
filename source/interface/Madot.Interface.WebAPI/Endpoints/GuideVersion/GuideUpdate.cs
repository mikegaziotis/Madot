using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using GuideVersion = Madot.Core.Domain.Models.GuideVersion;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideVersionUpdateRequest([FromBody] DTOs.Requests.GuideVersionUpdateCommand Command) : IRequest;

public class GuideVersionUpdateEndpoint(
    ICommandHandler<GuideVersionUpdateCommand, GuideVersion> handler): IEndpoint<GuideVersionUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionUpdateRequest request)
    {
        await handler.Handle(new GuideVersionUpdateCommand
        {
            Id = request.Command.Id,
            Data = request.Command.Data,
            IsDeleted = request.Command.IsDeleted
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] GuideVersionUpdateEndpoint endpoint, [AsParameters] GuideVersionUpdateRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void GuideVersionUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(GuideVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a GuideVersion record",
                Description = "Updates an GuideVersion record for the given specification",
                OperationId = "GuideVersionUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}