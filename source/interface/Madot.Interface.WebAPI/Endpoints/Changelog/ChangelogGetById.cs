using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using VersionedDocument = Madot.Core.Domain.Models.VersionedDocument;

namespace Madot.Interface.WebAPI.Endpoints;

public record ChangelogGetByIdRequest([FromRoute(Name="id")]string Id):IRequest;

public class ChangelogGetByIdEndpoint(
    IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?> handler,
    IMapper mapper): IEndpoint<ChangelogGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(ChangelogGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByIdQuery(request.Id, VersionedDocumentType.Changelog));
        if (result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.Changelog>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send([FromServices] ChangelogGetByIdEndpoint endpoint, [AsParameters] ChangelogGetByIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void ChangelogGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.Changelog>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(ChangelogTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a Changelog by its Id",
                Description = "Returns the Changelog for a given Id",
                OperationId = "ChangelogGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
} 