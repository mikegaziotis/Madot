using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentGetByIdQuery(string Id, VersionedDocumentType Type):IQuery;

public class VersionedDocumentGetByIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?>
{
    public async Task<VersionedDocument?> Handle(VersionedDocumentGetByIdQuery query)
    {
        return await dbDbContext.VersionedDocuments
            .Where(x => x.Id == query.Id)
            .Where(x=>x.DocumentType==query.Type)
            .FirstOrDefaultAsync();
    }
}