using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record OpenApiSpecUpdateRequest([FromBody] DTOs.Requests.VersionedDocumentUpdateCommand Command):IRequest;


public class OpenApiSpecUpdateEndpoint(
    ICommandHandler<VersionedDocumentUpdateCommand, VersionedDocument> handler): IEndpoint<OpenApiSpecUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(OpenApiSpecUpdateRequest request)
    {
        await handler.Handle(new VersionedDocumentUpdateCommand()
        {
            Id = request.Command.Id,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.OpenApiSpec
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] OpenApiSpecUpdateEndpoint endpoint, [AsParameters] OpenApiSpecUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void OpenApiSpecUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a OpenApiSpec record",
                Description = "Updates an OpenApiSpec record for the given specification",
                OperationId = "OpenApiSpecUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}