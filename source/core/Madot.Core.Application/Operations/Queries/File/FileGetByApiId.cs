using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Core.Application.Operations.Queries.File;

public record FileGetByApiIdQuery(string ApiId) : IQuery;

public class FileGetByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<FileGetByApiIdQuery,IEnumerable<Domain.Models.File>>
{
    public override async Task<IEnumerable<Domain.Models.File>> Handle(FileGetByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.Files
            .Include(x=>x.FileLinks)
            .Where(x => x.ApiId == query.ApiId)
            .ToListAsync());
    }
}