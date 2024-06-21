using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Badop.Core.Domain.Models.VersionedDocument;

namespace Badop.Shell.API.Endpoints;

public record HomepageGetByIdRequest([FromRoute(Name="id")]string Id):IRequest;

public class HomepageGetByIdMediator(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IMediator<HomepageGetByIdRequest,IResult>
{
    public async Task<IResult> Send(HomepageGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.Homepage));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Homepage>(result);
        return Results.Ok(finalResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> HomepageGetByIdEndpoint([FromServices] HomepageGetByIdMediator mediator, [AsParameters] HomepageGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void HomepageGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Homepage>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(HomepageTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a Homepage by its Id",
                Description = "Returns the Homepage for a given Id",
                OperationId = "HomepageGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 