using Badop.Core.Application.Operations.Queries;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Commands.Api;

public record ApiGetAllQuery(bool VisibleOnly=true): IQuery;

public class ApiGetAllQueryHandler(
    BadopContext dbContext): IQueryHandler<ApiGetAllQuery, IEnumerable<Domain.Models.Api>>
{
    public async Task<IEnumerable<Domain.Models.Api>> Handle(ApiGetAllQuery query)
    {
        if(query.VisibleOnly)
            return await dbContext.Apis.Where(x=>!x.IsHidden).ToListAsync(); 
        
        return await dbContext.Apis.ToListAsync();
    }
}