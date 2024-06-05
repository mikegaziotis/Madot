using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.File;

public record FileGetByApiIdQuery(string ApiId) : IQuery;

public class FileGetByApiId(
    BadopDbContext dbDbContext): IQueryHandler<FileGetByApiIdQuery,IEnumerable<Domain.Models.File>>
{
    public async Task<IEnumerable<Domain.Models.File>> Handle(FileGetByApiIdQuery query)
    {
        return await dbDbContext.Files.Where(x => x.ApiId == query.ApiId).ToListAsync();
    }
}