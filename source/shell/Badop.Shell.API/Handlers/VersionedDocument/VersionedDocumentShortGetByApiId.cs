using Badop.Core.Domain.Enums;
using Badop.Core.Domain.ShortTypes;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentShortGetByApiIdQuery(string ApiId, VersionedDocumentType Type):IQuery;

public class VersionedDocumentShortGetAllByApiIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<VersionedDocumentShortGetByApiIdQuery,IEnumerable<VersionedDocumentShort>>
{
    public async Task<IEnumerable<VersionedDocumentShort>> Handle(VersionedDocumentShortGetByApiIdQuery query)
    {
        return await dbDbContext.VersionedDocuments
            .Where(x => x.DocumentType == query.Type)
            .Where(x => x.ApiId == query.ApiId)
            .Select(x=>new VersionedDocumentShort(x))
            .ToListAsync();
    }
}