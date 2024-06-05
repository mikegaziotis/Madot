using Badop.Core.Application.Operations.Queries;

namespace Badop.Core.Application.Operations.Commands.File;

public record FileGetByIdQuery(string Id):IQuery;

public class FileGetByIdQueryHandler(
    BadopContext dbContext):IQueryHandler<FileGetByIdQuery,Domain.Models.File?>
{
    public async Task<Domain.Models.File?> Handle(FileGetByIdQuery query)
    {
        return await dbContext.Files.FindAsync(query.Id);
    }
}