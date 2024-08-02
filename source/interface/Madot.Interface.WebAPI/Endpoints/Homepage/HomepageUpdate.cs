using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record HomepageUpdateRequest([FromBody] DTOs.Requests.VersionedDocumentUpdateCommand Command):IRequest;


public class HomepageUpdateEndpoint(
    ICommandHandler<VersionedDocumentUpdateCommand, Core.Domain.Models.VersionedDocument> handler): IEndpoint<HomepageUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(HomepageUpdateRequest request)
    {
        await handler.Handle(new VersionedDocumentUpdateCommand()
        {
            Id = request.Command.Id,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.Homepage
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] HomepageUpdateEndpoint endpoint, [AsParameters] HomepageUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void HomepageUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a Homepage record",
                Description = "Updates an Homepage record for the given specification",
                OperationId = "HomepageUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}