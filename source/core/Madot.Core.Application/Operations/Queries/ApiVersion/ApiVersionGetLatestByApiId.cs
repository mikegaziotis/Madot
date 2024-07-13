using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.ApiVersion;

public record ApiVersionGetLatestByApiIdQuery(string ApiId, bool IncludeHidden=false):IQuery;

public class ApiVersionGetLatestByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<ApiVersionGetLatestByApiIdQuery,Domain.Models.ApiVersion?>
{
    public override async Task<Domain.Models.ApiVersion?> Handle(ApiVersionGetLatestByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.ApiVersions
            .Where(x => x.ApiId == query.ApiId)
            .Where(x => !x.IsHidden || query.IncludeHidden)
            .OrderByDescending(x => x.MajorVersion)
            .ThenByDescending(x => x.MinorVersion)
            .FirstOrDefaultAsync());
    }
}