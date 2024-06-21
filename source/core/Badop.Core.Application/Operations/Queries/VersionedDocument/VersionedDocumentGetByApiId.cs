using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentGetByApiIdQuery(string ApiId, VersionedDocumentType Type):IQuery;

public class VersionedDocumentShortGetAllByApiIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<VersionedDocumentGetByApiIdQuery,IEnumerable<VersionedDocument>>
{
    public async Task<IEnumerable<VersionedDocument>> Handle(VersionedDocumentGetByApiIdQuery query)
    {
        return await dbDbContext.VersionedDocuments
            .Where(x => x.DocumentType == query.Type)
            .Where(x => x.ApiId == query.ApiId)
            .ToListAsync();
    }
}