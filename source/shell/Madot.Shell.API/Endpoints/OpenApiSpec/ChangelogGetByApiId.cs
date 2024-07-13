using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.Homepage;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record OpenApiSpecGetByApiIdRequest([FromRoute(Name="api_id")]string ApiId) : IRequest;


public class OpenApiSpecGetByApiIdMediator(
    IQueryHandler<VersionedDocumentGetByApiIdQuery, IEnumerable<VersionedDocument>> handler,
    IMapper mapper) : IMediator<OpenApiSpecGetByApiIdRequest,IResult>
{
    public async Task<IResult> Send(OpenApiSpecGetByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiIdQuery(request.ApiId, VersionedDocumentType.OpenApiSpec));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.OpenApiSpec>).ToList();
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> OpenApiSpecGetByApiIdEndpoint([FromServices] OpenApiSpecGetByApiIdMediator mediator, [AsParameters] OpenApiSpecGetByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void OpenApiSpecGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.OpenApiSpec>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the OpenApiSpec history for an Api Id",
                Description = "Returns the OpenApiSpecs for a given Api_id",
                OperationId = "OpenApiSpecGetByApiId",
            })
            //add auth
            .AllowAnonymous();
    }
} 

