using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.OtherTypes;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record DocumentStatusGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class DocumentStatusGetByApiIdEndpoint(
    IQueryHandler<DocumentStatusGetByApiIdQuery, DocumentStatus?> handler,
    IMapper mapper) : IEndpoint<DocumentStatusGetByApiIdRequest, IResult>
{
    public async Task<IResult> Handle(DocumentStatusGetByApiIdRequest request)
    {
        var result = await handler.Handle(new DocumentStatusGetByApiIdQuery(request.ApiId));
        if (result is null)
            return Results.NotFound();
        return Results.Ok(result);
    }
    
    public static async Task<IResult> Send([FromServices] DocumentStatusGetByApiIdEndpoint endpoint, [AsParameters] DocumentStatusGetByApiIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void DocumentStatusGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DocumentStatus>()
            .Produces(StatusCodes.Status404NotFound)
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