using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Guide = Madot.Core.Domain.Models.Guide;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideUpdateRequest([FromBody] DTOs.Requests.GuideUpdateCommand Command) : IRequest;

public class GuideUpdateEndpoint(
    ICommandHandler<GuideUpdateCommand, Guide> handler): IEndpoint<GuideUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(GuideUpdateRequest request)
    {
        await handler.Handle(new GuideUpdateCommand
        {
            Id = request.Command.Id,
            IsDeleted = request.Command.IsDeleted,
            ProvisionalOrderId = request.Command.ProvisionalOrderId
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] GuideUpdateEndpoint endpoint, [AsParameters] GuideUpdateRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void GuideUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(GuideTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a Guide record",
                Description = "Updates an Guide record for the given specification",
                OperationId = "GuideUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}