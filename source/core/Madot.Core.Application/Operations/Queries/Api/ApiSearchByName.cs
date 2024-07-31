using Madot.Core.Application.Enums;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record ApiSearchByNameQuery(string SearchTerm, SearchMethod SearchMethod):IQuery;

public class ApiSearchByNameQueryHandler(
    MadotDbContext dbContext): IQueryHandler<ApiSearchByNameQuery,IEnumerable<Domain.Models.Api>>
{
    public override async Task<IEnumerable<Domain.Models.Api>> Handle(ApiSearchByNameQuery query)
    {
        var result = query.SearchMethod switch
        {
            SearchMethod.StartsWith => await SafeDbExecuteAsync(async () => 
                await dbContext.Apis
                .Where(x => x.DisplayName.StartsWith(query.SearchTerm))
                .ToListAsync()),
            SearchMethod.Contains => await SafeDbExecuteAsync(async () => 
                await dbContext.Apis
                .Where(x => x.DisplayName.Contains(query.SearchTerm))
                .ToListAsync()),
            SearchMethod.EndsWith => await SafeDbExecuteAsync(async () => 
                await dbContext.Apis
                .Where(x => x.DisplayName.EndsWith(query.SearchTerm))
                .ToListAsync()),
            SearchMethod.Exact => await SafeDbExecuteAsync(async() => 
                await dbContext.Apis
                .Where(x => x.DisplayName.Equals(query.SearchTerm))
                .ToListAsync()),
            _ => throw new ArgumentException("Not a valid search method")
        };
        return result;
    }
}