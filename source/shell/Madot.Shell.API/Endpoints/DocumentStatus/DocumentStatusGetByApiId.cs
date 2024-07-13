using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.DocumentStatus;
using Madot.Core.Domain.OtherTypes;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Shell.API.Endpoints;

public record DocumentStatusGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class DocumentStatusGetByApiIdMediator(
    IQueryHandler<DocumentStatusGetByApiIdQuery, DocumentStatus?> handler,
    IMapper mapper) : IMediator<DocumentStatusGetByApiIdRequest, IResult>
{
    public async Task<IResult> Send(DocumentStatusGetByApiIdRequest request)
    {
        var result = await handler.Handle(new DocumentStatusGetByApiIdQuery(request.ApiId));
        if (result is null)
            return Results.NotFound();
        return Results.Ok(result);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> DocumentStatusGetByApiIdEndpoint([FromServices] DocumentStatusGetByApiIdMediator mediator, [AsParameters] DocumentStatusGetByApiIdRequest request) 
         => await mediator.Send(request);
    
    public static void DocumentStatusGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DocumentStatus>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(DocumentStatusTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an Api's document status",
                Description = "Retrieve the document status for an Api by its Id",
                OperationId = "DocumentStatusGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}