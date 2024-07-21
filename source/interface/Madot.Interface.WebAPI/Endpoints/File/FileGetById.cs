using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Interface.WebAPI.Endpoints;
public record FileGetByIdRequest([FromRoute(Name="id")] string Id):IRequest;

public class FileGetByIdEndpoint(
    IQueryHandler<FileGetByIdQuery,File?> handler,
    IMapper mapper):IEndpoint<FileGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(FileGetByIdRequest request)
    {
        var result = await handler.Handle(new FileGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        var finalResult = mapper.Map<DTOs.Responses.File>(result);
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send(FileGetByIdEndpoint endpoint, [AsParameters] FileGetByIdRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void FileGetByIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<DTOs.Responses.File>()
            .Produces(StatusCodes.Status404NotFound)
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