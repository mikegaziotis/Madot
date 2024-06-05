using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.File;

public record FileGetByIdQuery(string Id):IQuery;

public class FileGetByIdQueryHandler(
    BadopDbContext dbDbContext):IQueryHandler<FileGetByIdQuery,Domain.Models.File?>
{
    public async Task<Domain.Models.File?> Handle(FileGetByIdQuery query)
    {
        return await dbDbContext.Files
            .Include(x=>x.FileLinks)
            .FirstOrDefaultAsync(x=>x.Id==query.Id);
    }
}