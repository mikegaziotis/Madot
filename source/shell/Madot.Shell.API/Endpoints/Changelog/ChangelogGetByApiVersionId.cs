using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Homepage;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Shell.API.Endpoints;

public record ChangelogGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class ChangelogGetByApiVersionIdMediator(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IMediator<ChangelogGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Send(ChangelogGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.Changelog));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Changelog>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> ChangelogGetByApiVersionIdEndpoint([FromServices] ChangelogGetByApiVersionIdMediator mediator, [AsParameters] ChangelogGetByApiVersionIdRequest request) 
        => await mediator.Send(request);
    
    public static void ChangelogGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Changelog>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Changelog for an Api Version",
                Description = "Returns the Changelog for a given Api_Version_id",
                OperationId = "ChangelogGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 