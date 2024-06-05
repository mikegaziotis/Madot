using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Badop.Core.Domain.Enums.OperatingSystem;

namespace Badop.Core.Application.Operations.Queries.File;

public record FileGetByApiIdQuery(string ApiId) : IQuery;

public class FileGetByApiIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<FileGetByApiIdQuery,IEnumerable<Domain.Models.File>>
{
    public async Task<IEnumerable<Domain.Models.File>> Handle(FileGetByApiIdQuery query)
    {
        return await dbDbContext.Files
            .Include(x=>x.FileLinks)
            .Where(x => x.ApiId == query.ApiId)
            .ToListAsync();

        
    }
}