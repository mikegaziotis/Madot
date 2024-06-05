using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.File;
using Microsoft.AspNetCore.Mvc;
using File = Badop.Core.Domain.Models.File;

namespace Badop.Shell.API.Handlers;
public record FileGetByIdRequest([FromRoute(Name="file_id")] string Id):IRequest;

public class FileGetByIdHandler(
    IQueryHandler<FileGetByIdQuery,File?> handler):IHandler<FileGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(FileGetByIdRequest request)
    {
        var result = await handler.Handle(new FileGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
}