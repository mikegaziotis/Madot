using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogInsertRequest([FromBody] DTOs.Requests.VersionedDocumentInsertCommand Command):IRequest;


public class ChangelogInsertEndpoint(
    ICommandHandler<VersionedDocumentInsertCommand, VersionedDocument, string> handler): IEndpoint<ChangelogInsertRequest,IResult> 
{
    public async Task<IResult> Handle(ChangelogInsertRequest request)
    {
        var appCommonPageId = await handler.Handle(new VersionedDocumentInsertCommand()
        {
            ApiId = request.Command.ApiId,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.Changelog,
            Iteration = 1
        });
        
        return Results.Ok(new StringIdCreated(appCommonPageId));
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogInsertEndpoint endpoint, [AsParameters] ChangelogInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ChangelogInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a Changelog record",
                Description = "Creates an Changelog record for the given specification",
                OperationId = "ChangelogInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}