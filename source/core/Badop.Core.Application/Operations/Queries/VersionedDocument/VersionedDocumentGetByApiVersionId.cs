using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record HomepageGetByApiVersionIdQuery(string ApiVersionId): IQuery;

public class HomepageGetByApiVersionIdQueryHandler(BadopContext dbContext): IQueryHandler<HomepageGetByApiVersionIdQuery,VersionedDocument?>
{
    public async Task<VersionedDocument?> Handle(HomepageGetByApiVersionIdQuery query)
    {
        return await dbContext.ApiVersions
            .Include(x => x.Homepage)
            .Where(x => x.Id == query.ApiVersionId)
            .Select(x => x.Homepage)
            .FirstOrDefaultAsync(); 
    }
}