using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record VersionedDocumentDataGetByIdRequest(string Id, VersionedDocumentType Type):IRequest;

public class VersionedDocumentDataGetByIdHandler(
    IQueryHandler<VersionedDocumentDataGetByIdQuery,string?> handler): IHandler<VersionedDocumentDataGetByIdRequest,IResult>
{
    public async Task<IResult> Handle(VersionedDocumentDataGetByIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentDataGetByIdQuery(request.Id, request.Type));
        if (string.IsNullOrEmpty(result))
            return Results.NotFound();
        
        return Results.Ok(result);
    }
}