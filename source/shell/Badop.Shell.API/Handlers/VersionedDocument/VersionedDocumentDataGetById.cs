using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentDataGetByIdQuery(string Id):IQuery;

public class VersionedDocumentDataGetByIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<VersionedDocumentDataGetByIdQuery,string?>
{
    public async Task<string?> Handle(VersionedDocumentDataGetByIdQuery query)
    {
        return await dbDbContext.VersionedDocuments
            .Where(x => x.Id == query.Id)
            .Select(x => x.Data)
            .FirstOrDefaultAsync();
    }
}