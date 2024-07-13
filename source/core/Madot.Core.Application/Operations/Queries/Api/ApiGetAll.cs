using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.Api;

public record ApiGetAllQuery(bool VisibleOnly=true): IQuery;

public class ApiGetAllQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<ApiGetAllQuery, IEnumerable<Domain.Models.Api>>
{
    public override async Task<IEnumerable<Domain.Models.Api>> Handle(ApiGetAllQuery query)
    {
        if (query.VisibleOnly)
            return await SafeDbExecuteAsync(async () => await dbDbContext.Apis.Where(x => !x.IsHidden).ToListAsync());

        return await SafeDbExecuteAsync(async () => await dbDbContext.Apis.ToListAsync());
    }
}

