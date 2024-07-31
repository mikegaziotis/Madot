using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;


public record AppCommonPagesGetAllQuery(bool includeDeleted): IQuery;

public class AppCommonPagesGetAllQueryHandler(
    MadotDbContext dbDbContext) : IQueryHandler<AppCommonPagesGetAllQuery,IEnumerable<Domain.Models.AppCommonPage>>
{
    public override async Task<IEnumerable<Domain.Models.AppCommonPage>> Handle(AppCommonPagesGetAllQuery query)
    {
        return await SafeDbExecuteAsync(async () => 
            await dbDbContext.AppCommonPages
            .Where(x => !x.IsDeleted || query.includeDeleted)
            .ToListAsync());
    }
}