using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class ChangelogGetByApiVersionIdEndpoint(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<ChangelogGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Handle(ChangelogGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.Changelog));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Changelog>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogGetByApiVersionIdEndpoint endpoint, [AsParameters] ChangelogGetByApiVersionIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ChangelogGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Changelog>()
            .Produces(StatusCodes.Status404NotFound)
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