using Badop.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentDataGetByIdQuery(string Id, VersionedDocumentType Type):IQuery;

public class VersionedDocumentDataGetByIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<VersionedDocumentDataGetByIdQuery,string?>
{
    public async Task<string?> Handle(VersionedDocumentDataGetByIdQuery query)
    {
        return await dbDbContext.VersionedDocuments
            .Where(x => x.Id == query.Id)
            .Where(x=>x.DocumentType==query.Type)
            .Select(x => x.Data)
            .FirstOrDefaultAsync();
    }
}