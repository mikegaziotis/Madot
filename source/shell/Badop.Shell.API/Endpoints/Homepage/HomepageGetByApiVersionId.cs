using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Badop.Core.Domain.Models.VersionedDocument;

namespace Badop.Shell.API.Endpoints;

public record HomepageGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class HomepageGetByApiVersionIdMediator(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler,
    IMapper mapper) : IMediator<HomepageGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Send(HomepageGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, VersionedDocumentType.Homepage));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Homepage>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> HomepageGetByApiVersionIdEndpoint([FromServices] HomepageGetByApiVersionIdMediator mediator, [AsParameters] HomepageGetByApiVersionIdRequest request) 
        => await mediator.Send(request);
    
    public static void HomepageGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Homepage>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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