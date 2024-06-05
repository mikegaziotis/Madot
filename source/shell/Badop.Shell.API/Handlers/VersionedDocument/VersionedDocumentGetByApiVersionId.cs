using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Homepage;
using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record VersionedDocumentGetByApiVersionIdRequest(string ApiVersionId, VersionedDocumentType Type): IRequest;

public class VersionedDocumentGetByApiVersionIdHandler(
    IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?> handler) : IHandler<VersionedDocumentGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Handle(VersionedDocumentGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new VersionedDocumentGetByApiVersionIdQuery(request.ApiVersionId, request.Type));
        if (result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
}