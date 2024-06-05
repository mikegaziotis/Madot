using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Badop.Core.Domain.ShortTypes;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record VersionedDocumentShortGetByApiIdRequest(string ApiId, VersionedDocumentType Type):IRequest;

public class VersionedDocumentShortGetByApiIdHandler(
    IQueryHandler<VersionedDocumentShortGetByApiIdQuery,IEnumerable<VersionedDocumentShort>> handler): IHandler<VersionedDocumentShortGetByApiIdRequest,IResult> 
{
    public async Task<IResult> Handle(VersionedDocumentShortGetByApiIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentShortGetByApiIdQuery(request.ApiId, request.Type));
        return Results.Ok(result);
    }
}