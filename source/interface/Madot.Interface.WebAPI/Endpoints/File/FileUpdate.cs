using Madot.Core.Application.Operations;
using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using File = Madot.Core.Domain.Models.File;

namespace Madot.Interface.WebAPI.Endpoints;

public record FileUpdateRequest([FromBody] DTOs.Requests.FileUpdateCommand Command) : IRequest;

public class FileUpdateEndpoint(
    ICommandHandler<FileUpdateCommand, File> handler): IEndpoint<FileUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(FileUpdateRequest request)
    {
        await handler.Handle(new FileUpdateCommand
        {
            Id = request.Command.Id,
            DisplayName = request.Command.DisplayName,
            Description = request.Command.Description,
            ImageUrl = request.Command.ImageUrl,
            IsDeleted = request.Command.IsDeleted,
            FileLinks = request.Command.FileLinks.ToList().Select(x=>new FileLinkItem
            {
                ChipArchitecture = x.ChipArchitecture,
                OperatingSystem = x.OperatingSystem,
                DownloadUrl = x.DownloadUrl
            }).ToArray(),
            
            
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] FileUpdateEndpoint endpoint, [AsParameters] FileUpdateRequest request) 
        => await endpoint.Handle(request);
}


public static partial class EndpointExtensions
{
    public static void FileUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open AppCommonPage description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(FileTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update a File record",
                Description = "Updates an File record for the given specification",
                OperationId = "FileUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}