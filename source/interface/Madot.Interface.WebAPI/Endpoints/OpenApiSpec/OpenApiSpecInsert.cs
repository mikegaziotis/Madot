using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record OpenApiSpecInsertRequest([FromBody] DTOs.Requests.VersionedDocumentInsertCommand Command):IRequest;


public class OpenApiSpecInsertEndpoint(
    ICommandHandler<VersionedDocumentInsertCommand, Core.Domain.Models.VersionedDocument, string> handler): IEndpoint<OpenApiSpecInsertRequest,IResult> 
{
    public async Task<IResult> Handle(OpenApiSpecInsertRequest request)
    {
        var oasId = await handler.Handle(new VersionedDocumentInsertCommand()
        {
            ApiId = request.Command.ApiId,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.OpenApiSpec,
            Iteration = 1
        });
        
        return Results.Ok(new StringIdCreated(oasId));
    }
    
    public static async Task<IResult> Send([FromServices] OpenApiSpecInsertEndpoint endpoint, [AsParameters] OpenApiSpecInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void OpenApiSpecInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces<StringIdCreated>()
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a OpenApiSpec record",
                Description = "Creates an OpenApiSpec record for the given specification",
                OperationId = "OpenApiSpecInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}