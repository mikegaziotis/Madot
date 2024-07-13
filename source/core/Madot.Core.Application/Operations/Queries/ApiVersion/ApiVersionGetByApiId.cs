using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.ApiVersion;

public record ApiVersionGetByApiIdQuery(string ApiId): IQuery;

public class ApiVersionGetByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<ApiVersionGetByApiIdQuery,IEnumerable<Domain.Models.ApiVersion>>
{
    public override async Task<IEnumerable<Domain.Models.ApiVersion>> Handle(ApiVersionGetByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.ApiVersions.Where(x => x.ApiId == query.ApiId).ToListAsync());
    }
}