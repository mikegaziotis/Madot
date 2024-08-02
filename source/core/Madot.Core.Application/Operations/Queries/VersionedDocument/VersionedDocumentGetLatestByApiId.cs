using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record VersionedDocumentGetLatestByApiIdQuery(string ApiId, VersionedDocumentType Type):IQuery;

public class VersionedDocumentGetLatestByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<VersionedDocumentGetLatestByApiIdQuery,VersionedDocument?>
{
    public override async Task<VersionedDocument?> Handle(VersionedDocumentGetLatestByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async ()=> await dbDbContext.VersionedDocuments
            .Where(x => x.DocumentType == query.Type)
            .Where(x => x.ApiId == query.ApiId)
            .OrderByDescending(x=>x.Iteration)
            .FirstOrDefaultAsync());
    }
}