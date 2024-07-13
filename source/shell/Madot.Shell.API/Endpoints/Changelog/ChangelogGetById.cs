using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Homepage;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Shell.API.Endpoints;

public record ChangelogGetByIdRequest([FromRoute(Name="id")]string Id):IRequest;

public class ChangelogGetByIdMediator(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IMediator<ChangelogGetByIdRequest,IResult>
{
    public async Task<IResult> Send(ChangelogGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.Changelog));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Changelog>(result);
        return Results.Ok(finalResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> ChangelogGetByIdEndpoint([FromServices] ChangelogGetByIdMediator mediator, [AsParameters] ChangelogGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void ChangelogGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Changelog>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a Changelog by its Id",
                Description = "Returns the Changelog for a given Id",
                OperationId = "ChangelogGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 