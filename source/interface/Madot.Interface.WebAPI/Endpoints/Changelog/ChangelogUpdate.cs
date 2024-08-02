using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogUpdateRequest([FromBody] DTOs.Requests.VersionedDocumentUpdateCommand Command):IRequest;


public class ChangelogUpdateEndpoint(
    ICommandHandler<VersionedDocumentUpdateCommand, Core.Domain.Models.VersionedDocument> handler): IEndpoint<ChangelogUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(ChangelogUpdateRequest request)
    {
        await handler.Handle(new VersionedDocumentUpdateCommand()
        {
            Id = request.Command.Id,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.Changelog
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogUpdateEndpoint endpoint, [AsParameters] ChangelogUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ChangelogUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a Changelog record",
                Description = "Updates an Changelog record for the given specification",
                OperationId = "ChangelogUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}