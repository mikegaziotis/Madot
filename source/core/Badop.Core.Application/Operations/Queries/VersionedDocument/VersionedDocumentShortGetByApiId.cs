using Badop.Core.Domain.Enums;
using Badop.Core.Domain.ShortTypes;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentShortGetAllByApiIdQuery(string ApiId, VersionedDocumentType Type):IQuery;

public class VersionedDocumentShortGetAllByApiIdQueryHandler(
    BadopContext dbContext): IQueryHandler<VersionedDocumentShortGetAllByApiIdQuery,IEnumerable<VersionedDocumentShort>>
{
    public async Task<IEnumerable<VersionedDocumentShort>> Handle(VersionedDocumentShortGetAllByApiIdQuery query)
    {
        return await dbContext.VersionedDocuments
            .Where(x => x.DocumentType == query.Type)
            .Where(x => x.ApiId == query.ApiId)
            .Select(x=>new VersionedDocumentShort(x))
            .ToListAsync();
    }
}