using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Madot.Interface.WebAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record HomepageInsertRequest([FromBody] DTOs.Requests.VersionedDocumentInsertCommand Command):IRequest;


public class HomepageInsertEndpoint(
    ICommandHandler<VersionedDocumentInsertCommand, Core.Domain.Models.VersionedDocument, string> handler): IEndpoint<HomepageInsertRequest,IResult> 
{
    public async Task<IResult> Handle(HomepageInsertRequest request)
    {
        var homepageId = await handler.Handle(new VersionedDocumentInsertCommand()
        {
            ApiId = request.Command.ApiId,
            Data = request.Command.Data,
            DocumentType = VersionedDocumentType.Homepage,
            Iteration = 1
        });
        
        return Results.Ok(new StringIdCreated(homepageId));
    }
    
    public static async Task<IResult> Send([FromServices] HomepageInsertEndpoint endpoint, [AsParameters] HomepageInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void HomepageInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces<StringIdCreated>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a Homepage record",
                Description = "Creates an Homepage record for the given specification",
                OperationId = "HomepageInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}