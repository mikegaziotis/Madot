using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.DocumentStatus;
using Madot.Core.Domain.OtherTypes;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record DocumentStatusGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId): IRequest;

public class DocumentStatusGetByApiVersionIdMediator(
    IQueryHandler<DocumentStatusGetByApiVersionIdQuery, DocumentStatus?> handler,
    IMapper mapper) : IMediator<DocumentStatusGetByApiVersionIdRequest, IResult>
{
    public async Task<IResult> Send(DocumentStatusGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new DocumentStatusGetByApiVersionIdQuery(request.ApiVersionId));
        if (result is null)
            return Results.NotFound();
        return Results.Ok(result);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> DocumentStatusGetByApiVersionIdEndpoint([FromServices] DocumentStatusGetByApiVersionIdMediator mediator, [AsParameters] DocumentStatusGetByApiVersionIdRequest request) 
         => await mediator.Send(request);
    
    public static void DocumentStatusGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DocumentStatus>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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