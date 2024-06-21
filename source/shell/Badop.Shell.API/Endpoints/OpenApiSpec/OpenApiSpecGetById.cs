using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Badop.Core.Domain.Models.VersionedDocument;

namespace Badop.Shell.API.Endpoints;

public record OpenApiSpecGetByIdRequest([FromRoute(Name = "id")]string Id):IRequest;

public class OpenApiSpecGetByIdMediator(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IMediator<OpenApiSpecGetByIdRequest,IResult>
{
    public async Task<IResult> Send(OpenApiSpecGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.OpenApiSpec));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.OpenApiSpec>(result);
        return Results.Ok(finalResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> OpenApiSpecGetByIdEndpoint([FromServices] OpenApiSpecGetByIdMediator mediator, [AsParameters] OpenApiSpecGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void OpenApiSpecGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.OpenApiSpec>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(OpenApiSpecTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get an OpenApiSpec by its Id",
                Description = "Returns the OpenApiSpec for a given Id",
                OperationId = "OpenApiSpecGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 