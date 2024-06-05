using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.ApiVersion;

public record ApiVersionGetByApiIdQuery(string ApiId): IQuery;

public class ApiVersionGetByApiIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<ApiVersionGetByApiIdQuery,IEnumerable<Domain.Models.ApiVersion>>
{
    public async Task<IEnumerable<Domain.Models.ApiVersion>> Handle(ApiVersionGetByApiIdQuery query)
    {
        return await dbDbContext.ApiVersions.Where(x => x.ApiId == query.ApiId).ToListAsync();
    }
}