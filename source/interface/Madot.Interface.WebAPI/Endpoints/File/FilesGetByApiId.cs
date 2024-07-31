using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Interface.WebAPI.Endpoints;
public record FilesGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class FilesGetByApiIdEndpoint(
    IQueryHandler<FileGetByApiIdQuery,IEnumerable<File>> handler,
    IMapper mapper):IEndpoint<FilesGetByApiIdRequest,IResult> 
{
    public async Task<IResult> Handle(FilesGetByApiIdRequest request)
    {
        var result = await handler.Handle(new FileGetByApiIdQuery(request.ApiId));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.File>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send(FilesGetByApiIdEndpoint endpoint, [AsParameters] FilesGetByApiIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void FilesGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.File>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(FileTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get the list of files for an Api",
                Description = "Returns the files and its links, given a Api_Id",
                OperationId = "FilesGetByApiId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}