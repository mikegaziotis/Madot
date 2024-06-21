using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Api;

public record ApiGetAllQuery(bool VisibleOnly=true): IQuery;

public class ApiGetAllQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<ApiGetAllQuery, IEnumerable<Domain.Models.Api>>
{
    public async Task<IEnumerable<Domain.Models.Api>> Handle(ApiGetAllQuery query)
    {
        if(query.VisibleOnly)
            return await dbDbContext.Apis.Where(x=>!x.IsHidden).ToListAsync(); 
        
        return await dbDbContext.Apis.ToListAsync();
    }
}

