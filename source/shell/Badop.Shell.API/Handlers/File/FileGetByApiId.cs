using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.File;
using Microsoft.AspNetCore.Mvc;
using File = Badop.Core.Domain.Models.File;

namespace Badop.Shell.API.Handlers;
public record FileGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class FileGetByApiIdHandler(
    IQueryHandler<FileGetByApiIdQuery,IEnumerable<File>> handler):IHandler<FileGetByApiIdRequest,IResult> 
{
    public async Task<IResult> Handle(FileGetByApiIdRequest request)
    {
        var result = await handler.Handle(new FileGetByApiIdQuery(request.ApiId));
        return Results.Ok(result);
    }
}