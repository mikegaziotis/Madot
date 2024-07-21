using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Madot.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Core.Application.Operations.Commands;

public record FileUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
    
    public string? Description { get; init; }

    public string? ImageUrl { get; init; }
    public bool IsDeleted { get; init; }

    public required FileLinkItem[] FileLinks { get; init; }
}

public class FileUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<FileUpdateCommand,Domain.Models.File>
{
    public override async Task Handle(FileUpdateCommand command)
    {
        var file = await SafeDbExecuteAsync(async () => await dbContext.Files.FirstOrDefaultAsync(x=>x.Id==command.Id));
        if (file is null)
            throw new EntityNotFoundException("An File with that Id does not exist");

        if (!command.IsDeleted)
        {
            if(await SafeDbExecuteAsync(async () => 
                   await dbContext.Files.AnyAsync(x=>x.ApiId==file.ApiId && x.DisplayName == command.DisplayName && x.Id!=command.Id)));
            {
                throw new InvalidArgumentException("A different File with the same DisplayName already exists. DisplayName must be unique");
            }
        }

        var entity = GetEntity(command, file);
            
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.Files.Update(entity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update File entity");
        
        var existingFileLinks = await SafeDbExecuteAsync(async () => await dbContext.FileLinks.Where(x=>x.FileId==command.Id).ToListAsync());
        
        foreach (var existingFileLink in existingFileLinks)
        {
            if (!command.FileLinks.Any(x =>
                    x.OperatingSystem == existingFileLink.OperatingSystem &&
                    x.ChipArchitecture == existingFileLink.ChipArchitecture))
            {
                _ = await SafeDbExecuteAsync(async () =>
                {
                    dbContext.FileLinks.Remove(existingFileLink);
                    await dbContext.SaveChangesAsync();
                    return true;
                });
            }
        }
        
        foreach (var commandFileLink in command.FileLinks)
        {
            var existingFileLink = existingFileLinks.FirstOrDefault(x =>
                x.OperatingSystem == commandFileLink.OperatingSystem &&
                x.ChipArchitecture == commandFileLink.ChipArchitecture);
            _ = await SafeDbExecuteAsync(async () =>
                {
                    dbContext.FileLinks.Update(GetFileLinkEntity(file.Id, commandFileLink, existingFileLink));
                    await dbContext.SaveChangesAsync();
                    return true;
                });
        }
    }

    public override Domain.Models.File GetEntity(FileUpdateCommand command, Domain.Models.File? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
     
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();

        return existingEntity with
        {
            DisplayName = command.DisplayName,
            IsDeleted = command.IsDeleted,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
    
    public Domain.Models.FileLink GetFileLinkEntity(string fileId, FileLinkItem commandItem, Domain.Models.FileLink? existingEntity)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        if (existingEntity is null)
        {
            return new Domain.Models.FileLink
            {
                FileId = fileId,
                OperatingSystem = commandItem.OperatingSystem,
                ChipArchitecture = commandItem.ChipArchitecture,
                DownloadUrl = commandItem.DownloadUrl,
                CreatedBy = userId,
                CreatedDate = timestamp,
                LastModifiedBy = userId,
                LastModifiedDate = timestamp
            };    
        }

        return existingEntity with
        {
            DownloadUrl = commandItem.DownloadUrl,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}