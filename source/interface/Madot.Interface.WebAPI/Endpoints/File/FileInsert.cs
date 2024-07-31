using Madot.Core.Application.Operations;
using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Interface.WebAPI.Endpoints;

public record FileInsertRequest([FromBody] DTOs.Requests.FileInsertCommand Command) : IRequest;

public class FileInsertEndpoint(
    ICommandHandler<FileInsertCommand, File, string> handler): IEndpoint<FileInsertRequest,IResult> 
{
    public async Task<IResult> Handle(FileInsertRequest request)
    {
        var appCommonPageId = await handler.Handle(new FileInsertCommand
        {
            DisplayName = request.Command.DisplayName,
            Description = request.Command.Description,
            ImageUrl = request.Command.ImageUrl,
            FileLinks = request.Command.FileLinks.ToList().Select(x=>new FileLinkItem
            {
                ChipArchitecture = x.ChipArchitecture,
                OperatingSystem = x.OperatingSystem,
                DownloadUrl = x.DownloadUrl
            }).ToArray(),
            ApiId = request.Command.ApiId,
            
        });
        
        return Results.Ok(new StringIdCreated(appCommonPageId));
    }
    
    public static async Task<IResult> Send([FromServices] FileInsertEndpoint endpoint, [AsParameters] FileInsertRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void FileInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(FileTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create a File record",
                Description = "Creates an File record for the given specification",
                OperationId = "FileInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}