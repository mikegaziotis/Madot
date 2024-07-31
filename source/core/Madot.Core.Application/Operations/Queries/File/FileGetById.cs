using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record FileGetByIdQuery(string Id):IQuery;

public class FileGetByIdQueryHandler(
    MadotDbContext dbDbContext):IQueryHandler<FileGetByIdQuery,Domain.Models.File?>
{
    public override async Task<Domain.Models.File?> Handle(FileGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.Files
            .Include(x=>x.FileLinks)
            .FirstOrDefaultAsync(x=>x.Id==query.Id));
    }
}