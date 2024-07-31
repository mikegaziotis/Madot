using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using GuideVersion = Madot.Core.Domain.Models.GuideVersion;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideVersionInsertRequest([FromBody] DTOs.Requests.GuideVersionInsertCommand Command) : IRequest;

public class GuideVersionInsertEndpoint(
    ICommandHandler<GuideVersionInsertCommand, GuideVersion, string> handler): IEndpoint<GuideVersionInsertRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionInsertRequest request)
    {
        var appCommonPageId = await handler.Handle(new GuideVersionInsertCommand
        {
            GuideId = request.Command.GuideId,
            Data = request.Command.Data
        });
        
        return Results.Ok(new StringIdCreated(appCommonPageId));
    }
    
    public static async Task<IResult> Send([FromServices] GuideVersionInsertEndpoint endpoint, [AsParameters] GuideVersionInsertRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void GuideVersionInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(GuideVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a GuideVersion record",
                Description = "Creates an GuideVersion record for the given specification",
                OperationId = "GuideVersionInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}