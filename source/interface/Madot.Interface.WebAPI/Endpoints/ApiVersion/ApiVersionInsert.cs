using AutoMapper;
using Madot.Core.Application.Operations;
using Madot.Core.Application.Operations.Commands;
using Madot.Interface.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using ApiVersion = Madot.Core.Domain.Models.ApiVersion;

namespace Madot.Interface.WebAPI.Endpoints;

public record ApiVersionInsertRequest([FromBody] DTOs.Requests.ApiVersionInsertCommand Command) : IRequest;

public class ApiVersionInsertEndpoint(
    ICommandHandler<ApiVersionInsertCommand, ApiVersion, string> handler,
    IMapper mapper): IEndpoint<ApiVersionInsertRequest,IResult> 
{
    public async Task<IResult> Handle(ApiVersionInsertRequest request)
    {
        var apiVersionId = await handler.Handle(new ApiVersionInsertCommand
        {
            ApiId = request.Command.ApiId,
            MajorVersion = request.Command.MajorVersion,
            MinorVersion = request.Command.MinorVersion,
            BuildOrReleaseTag = request.Command.BuildOrReleaseTag,
            OpenApiSpecId = request.Command.OpenApiSpecId,
            HomepageId = request.Command.HomepageId,
            ChangelogId = request.Command.ChangelogId,
            IsHidden = request.Command.IsHidden,
            IsBeta = request.Command.IsBeta,
            FileItems = request.Command.FileItems.Select(mapper.Map<FileItem>).ToArray(),
            GuideVersionItems = request.Command.FileItems.Select(mapper.Map<GuideVersionItem>).ToArray(),
        });
        
        return Results.Ok(new StringIdCreated(apiVersionId));
    }
    
    public static async Task<IResult> Send([FromServices] ApiVersionInsertEndpoint endpoint, [AsParameters] ApiVersionInsertRequest request) 
        => await endpoint.Handle(request);
}

public static partial class EndpointExtensions
{
    public static void ApiVersionInsertEndpointConfiguration(this RouteHandlerBuilder builder)
    {
        builder
            //add open api description
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status409Conflict)
            .WithTags(ApiVersionTag)
            .WithOpenApi(op=>new(op)
            {
                Summary = "Create an APIVersion record",
                Description = "Creates an API record for the given specification",
                OperationId = "ApiVersionInsert" ,
            })
            //add auth
            .AllowAnonymous();
    }
}