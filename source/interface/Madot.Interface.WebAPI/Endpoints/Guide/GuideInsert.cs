using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Guide = Madot.Core.Domain.Models.Guide;

namespace Madot.Interface.WebAPI.Endpoints;

public record GuideInsertRequest([FromBody] DTOs.Requests.GuideInsertCommand Command) : IRequest;

public class GuideInsertEndpoint(
    ICommandHandler<GuideInsertCommand, Guide, string> handler): IEndpoint<GuideInsertRequest,IResult> 
{
    public async Task<IResult> Handle(GuideInsertRequest request)
    {
        var guideId = await handler.Handle(new GuideInsertCommand
        {
            ApiId = request.Command.ApiId,
            Title = request.Command.Title,
            ProvisionalOrderId = request.Command.ProvisionalOrderId
        });
        
        return Results.Ok(new StringIdCreated(guideId));
    }
    
    public static async Task<IResult> Send([FromServices] GuideInsertEndpoint endpoint, [AsParameters] GuideInsertRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void GuideInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces<StringIdCreated>()
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(GuideTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a Guide record",
                Description = "Creates an Guide record for the given specification",
                OperationId = "GuideInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}