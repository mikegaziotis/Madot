using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentGetByIdQuery(string Id, VersionedDocumentType Type):IQuery;

public class VersionedDocumentGetByIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<VersionedDocumentGetByIdQuery,VersionedDocument?>
{
    public override async Task<VersionedDocument?> Handle(VersionedDocumentGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async ()=> await dbDbContext.VersionedDocuments
            .Where(x => x.Id == query.Id)
            .Where(x=>x.DocumentType==query.Type)
            .FirstOrDefaultAsync());
    }
}