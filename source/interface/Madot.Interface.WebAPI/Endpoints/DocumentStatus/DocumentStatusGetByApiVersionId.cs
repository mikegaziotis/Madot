using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.OtherTypes;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record DocumentStatusGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class DocumentStatusGetByApiVersionIdEndpoint(
    IQueryHandler<DocumentStatusGetByApiVersionIdQuery, DocumentStatus?> handler,
    IMapper mapper) : IEndpoint<DocumentStatusGetByApiVersionIdRequest, IResult>
{
    public async Task<IResult> Handle(DocumentStatusGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new DocumentStatusGetByApiVersionIdQuery(request.ApiVersionId));
        if (result is null)
            return Results.NotFound();
        return Results.Ok(result);
    }
    
    public static async Task<IResult> Send([FromServices] DocumentStatusGetByApiVersionIdEndpoint endpoint, [AsParameters] DocumentStatusGetByApiVersionIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void DocumentStatusGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DocumentStatus>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(DocumentStatusTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an ApiVersion's document status",
                Description = "Retrieve the document status for an ApiVersion by its Id",
                OperationId = "DocumentStatusGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}