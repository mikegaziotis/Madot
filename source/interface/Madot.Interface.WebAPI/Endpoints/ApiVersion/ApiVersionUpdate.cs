using AutoMapper;
using Madot.Core.Application.Operations;
using Madot.Core.Application.Operations.Commands;
using Madot.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiVersionUpdateRequest([FromBody] DTOs.Requests.ApiVersionUpdateCommand Command) : IRequest;

public class ApiVersionUpdateEndpoint(
    ICommandHandler<ApiVersionUpdateCommand, ApiVersion> handler,
    IMapper mapper): IEndpoint<ApiVersionUpdateRequest,IResult> 
{
    public async Task<IResult> Handle(ApiVersionUpdateRequest request)
    {
        await handler.Handle(new ApiVersionUpdateCommand
        {
            Id = request.Command.Id,
            BuildOrReleaseTag = request.Command.BuildOrReleaseTag,
            OpenApiSpecId = request.Command.OpenApiSpecId,
            HomepageId = request.Command.HomepageId,
            ChangelogId = request.Command.ChangelogId,
            IsHidden = request.Command.IsHidden,
            IsBeta = request.Command.IsBeta,
            FileItems = request.Command.FileItems.Select(mapper.Map<FileItem>).ToArray(),
            GuideVersionItems = request.Command.FileItems.Select(mapper.Map<GuideVersionItem>).ToArray(),
        });
        
        return Results.Ok();
    }
    
    public static async Task<IResult> Send([FromServices] ApiVersionUpdateEndpoint endpoint, [AsParameters] ApiVersionUpdateRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiVersionUpdateEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ApiVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Update an APIVersion record",
                Description = "Modifies an APIVersion record to match the given specification",
                OperationId = "ApiVersionUpdate" ,
            })
            //add auth
            .AllowAnonymous();
    }
}