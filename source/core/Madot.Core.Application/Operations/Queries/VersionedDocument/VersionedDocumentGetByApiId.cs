using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record VersionedDocumentGetByApiIdQuery(string ApiId, VersionedDocumentType Type):IQuery;

public class VersionedDocumentGetByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<VersionedDocumentGetByApiIdQuery,IEnumerable<VersionedDocument>>
{
    public override async Task<IEnumerable<VersionedDocument>> Handle(VersionedDocumentGetByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async ()=> await dbDbContext.VersionedDocuments
            .Where(x => x.DocumentType == query.Type)
            .Where(x => x.ApiId == query.ApiId)
            .ToListAsync());
    }
}