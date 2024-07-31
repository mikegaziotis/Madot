using AutoMapper;
using Madot.Core.Application.Operations.Queries;
using Madot.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Interface.WebAPI.Endpoints;
public record FilesGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId,
    [FromQuery] OperatingSystem OperatingSystem = OperatingSystem.Any, 
    [FromQuery] ChipArchitecture ChipArchitecture = ChipArchitecture.Any): IRequest;

public class FilesGetByApiVersionIdEndpoint(
    IQueryHandler<FileGetByApiVersionIdQuery,IEnumerable<File>> handler,
    IMapper mapper):IEndpoint<FilesGetByApiVersionIdRequest,IResult> 
{
    public async Task<IResult> Handle(FilesGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new FileGetByApiVersionIdQuery(request.ApiVersionId, request.OperatingSystem, request.ChipArchitecture));
        var finalResult = result.Select(mapper.Map<DTOs.Responses.File>).ToList();
        return Results.Ok(finalResult);
    }
    
    public static async Task<IResult> Send(FilesGetByApiVersionIdEndpoint endpoint, [AsParameters] FilesGetByApiVersionIdRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
   
    public static void FilesGetByApiVersionIdEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces<IEnumerable<DTOs.Responses.File>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(FileTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Get a list of Files for an APi Version",
                Description = "Returns the files and its links, for a given Api_Version_Id",
                OperationId = "FilesGetByApiVersionId" ,
            })
            //add auth
            .AllowAnonymous();
    }
}