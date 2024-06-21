using Badop.Core.Application.Enums;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Api;

public record ApiSearchByNameQuery(string SearchTerm, SearchMethod SearchMethod):IQuery;

public class ApiSearchByNameQueryHandler(
    BadopDbContext dbContext): IQueryHandler<ApiSearchByNameQuery,IEnumerable<Domain.Models.Api>>
{
    public async Task<IEnumerable<Domain.Models.Api>> Handle(ApiSearchByNameQuery query)
    {
        try
        {
            var result = query.SearchMethod switch
            {
                SearchMethod.StartsWith => await dbContext.Apis
                    .Where(x => x.DisplayName.StartsWith(query.SearchTerm))
                    .ToListAsync(),
                SearchMethod.Contains => await dbContext.Apis
                    .Where(x => x.DisplayName.Contains(query.SearchTerm))
                    .ToListAsync(),
                SearchMethod.EndsWith => await dbContext.Apis
                    .Where(x => x.DisplayName.EndsWith(query.SearchTerm))
                    .ToListAsync(),
                SearchMethod.Exact => await dbContext.Apis
                    .Where(x => x.DisplayName.Equals(query.SearchTerm))
                    .ToListAsync(),
                _ => throw new ArgumentException("Not a valid search method")
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}