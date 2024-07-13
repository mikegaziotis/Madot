using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Application.Operations.Queries.File;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Shell.API.Endpoints;
public record FilesGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class FilesGetByApiIdMediator(
    IQueryHandler<FileGetByApiIdQuery,IEnumerable<File>> handler,
    IMapper mapper):IMediator<FilesGetByApiIdRequest,IResult> 
{
    public async Task<IResult> Send(FilesGetByApiIdRequest request)
    {
        var result = await handler.Handle(new FileGetByApiIdQuery(request.ApiId));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.File>).ToList();
        return Results.Ok(finalResult);
    }
}


public static partial class EndpointExtensions
{
    public static async Task<IResult> FilesGetByApiIdEndpoint(FilesGetByApiIdMediator mediator, [AsParameters] FilesGetByApiIdRequest request) 
        => await mediator.Send(request);
    
    public static void FilesGetByApiIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.File>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
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