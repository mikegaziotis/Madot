using AutoMapper;
using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.File;
using Microsoft.AspNetCore.Mvc;
using File = Badop.Core.Domain.Models.File;

namespace Badop.Shell.API.Endpoints;
public record FileGetByIdRequest([FromRoute(Name="id")] string Id):IRequest;

public class FileGetByIdMediator(
    IQueryHandler<FileGetByIdQuery,File?> handler,
    IMapper mapper):IMediator<FileGetByIdRequest,IResult>
{
    public async Task<IResult> Send(FileGetByIdRequest request)
    {
        var result = await handler.Handle(new FileGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.File>(result);
        return Results.Ok(finalResult);
    }
}

public static partial class EndpointExtensions
{
    public static async Task<IResult> FileGetByIdEndpoint(FileGetByIdMediator mediator, [AsParameters] FileGetByIdRequest request) 
        => await mediator.Send(request);
    
    public static void FileGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.File>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(FileTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a File by its id",
                Description = "Returns the file and its links, given a file Id",
                OperationId = "FileGetById" ,
            })
            //add auth
            .AllowAnonymous();
    }
}