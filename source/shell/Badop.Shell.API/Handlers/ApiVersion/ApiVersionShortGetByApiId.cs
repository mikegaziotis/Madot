using Badop.Core.Domain.ShortTypes;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.ApiVersion;

public record ApiVersionShortGetByApiIdQuery(string ApiId, bool IncludeHidden=false):IQuery;

public class ApiVersionShortGetByApiIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<ApiVersionShortGetByApiIdQuery, IEnumerable<ApiVersionShort>>
{
    public async Task<IEnumerable<ApiVersionShort>> Handle(ApiVersionShortGetByApiIdQuery query)
    {
        return await dbDbContext.ApiVersions
            .Where(x => x.ApiId == query.ApiId)
            .Where(x=> !x.IsHidden || query.IncludeHidden)
            .Select(x => new ApiVersionShort(x))
            .ToListAsync();
    }
}