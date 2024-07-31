using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record HomepageGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class HomepageGetByApiVersionIdEndpoint(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IEndpoint<HomepageGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Handle(HomepageGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.Homepage));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Homepage>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] HomepageGetByApiVersionIdEndpoint endpoint, [AsParameters] HomepageGetByApiVersionIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void HomepageGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Homepage>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the Homepage for an Api Version",
                Description = "Returns the Homepage for a give Api_Version_id",
                OperationId = "HomepageGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 