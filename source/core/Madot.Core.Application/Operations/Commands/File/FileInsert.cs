using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Core.Application.Operations.Commands.File;

public class FileInsertCommand:ICommand
{
    public required string ApiId { get; init; }
    public required string DisplayName { get; init; }
    
    public string? Description { get; init; }

    public string? ImageUrl { get; init; }

    public required FileLinkItem[] FileLinks { get; init; }
    public record FileLinkItem
    {
        public required OperatingSystem OperatingSystem { get; init; }

        public required ChipArchitecture ChipArchitecture { get; init; }

        public required string DownloadUrl { get; init; }
    }
}

public class FileInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<FileInsertCommand,Domain.Models.File, string>
{
    public override async Task<string> Handle(FileInsertCommand command)
    {
        var alreadyExists = await SafeDbExecuteAsync(async()=>await dbContext.Files.AnyAsync(x => x.DisplayName == command.DisplayName && !x.IsDeleted));
        if (alreadyExists)
        {
            throw new EntityConflictException("File with the same DisplayName already exists.");
        }

        if (command.FileLinks.ToList().GroupBy(x => new { x.OperatingSystem, x.ChipArchitecture }).Count() > 1)
            throw new InvalidArgumentException("The are duplicate FileLik items in the input");

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async() =>
        {
            dbContext.Files.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result > 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new File entity");
        return newEntity.Id;
    }

    public override Domain.Models.File GetEntity(FileInsertCommand command, Domain.Models.File? existingEntity =null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        var fileId = keyProvider.GenerateRandomKey();
            
        var newFile = new Domain.Models.File
        {
            Id = fileId,
            ApiId = command.ApiId,
            DisplayName = command.DisplayName,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            IsDeleted = false,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
        foreach (var item in command.FileLinks)
        {
            newFile.FileLinks.Add(new FileLink
            {
                FileId = fileId,
                OperatingSystem = item.OperatingSystem,
                ChipArchitecture = item.ChipArchitecture,
                DownloadUrl = item.DownloadUrl,
                CreatedBy = userId,
                CreatedDate = timestamp,
                LastModifiedBy = userId,
                LastModifiedDate = timestamp
            });    
        }

        return newFile;
    }
}