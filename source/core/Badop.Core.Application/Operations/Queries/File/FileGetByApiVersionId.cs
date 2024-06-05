using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Badop.Core.Domain.Enums.OperatingSystem;

namespace Badop.Core.Application.Operations.Queries.ApiVersionFile;

public record FileGetByApiVersionIdQuery(string ApiVersionId, OperatingSystem OperatingSystem= OperatingSystem.Any, ChipArchitecture ChipArchitecture = ChipArchitecture.Any) : IQuery;

public class FileGetByApiVersionIdQueryHandler(
    BadopDbContext dbContext):IQueryHandler<FileGetByApiVersionIdQuery,IEnumerable<Domain.Models.File>>
{
    public async Task<IEnumerable<Domain.Models.File>> Handle(FileGetByApiVersionIdQuery query)
    {
        var result = await dbContext.ApiVersionFiles
            .Include(x => x.File)
            .Include(x => x.File.FileLinks)
            .Where(x => x.ApiVersionId == query.ApiVersionId)
            .Select(x=>x.File)
            .ToListAsync();
        
        if (query.OperatingSystem != OperatingSystem.Any || query.ChipArchitecture != ChipArchitecture.Any)
        {
            result.ForEach(x =>
            {
                var toRemove = new List<FileLink>();
                if (query.OperatingSystem != OperatingSystem.Any)
                {
                    toRemove.AddRange(x.FileLinks.Where(y => y.OperatingSystem != query.OperatingSystem && y.OperatingSystem!=OperatingSystem.Any));
                }

                if (query.ChipArchitecture != ChipArchitecture.Any)
                {
                    toRemove.AddRange(x.FileLinks.Where(y => y.ChipArchitecture != query.ChipArchitecture && y.ChipArchitecture!=ChipArchitecture.Any));
                }

                if (toRemove.Any())
                {
                    foreach (var fileLink in toRemove)
                    {
                        x.FileLinks.Remove(fileLink);        
                    }
                }
            });
        }

        return result;
    }
}
