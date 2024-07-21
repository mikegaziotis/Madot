using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands;

public record ApiVersionUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }

    public bool IsHidden { get; init; }
    
    public GuideVersionItem[] GuideVersionItems { get; init; } = [];
    
    public FileItem[] FileItems { get; init; } = [];
}

public class ApiVersionUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiVersionUpdateCommand,ApiVersion>
{
    public override async Task Handle(ApiVersionUpdateCommand command)
    {
        var apiVersion = await SafeDbExecuteAsync(async () => await dbContext.ApiVersions.FindAsync(command.Id));
        if (apiVersion is null)
            throw new EntityNotFoundException("An ApiVersion with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersions.Update(GetEntity(command, apiVersion));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update Api entity");

        await UpdateFiles(command.Id, command.FileItems);
        await UpdateGuideVersions(command.Id, command.GuideVersionItems);
    }

    public override ApiVersion GetEntity(ApiVersionUpdateCommand command, ApiVersion? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            BuildOrReleaseTag = command.BuildOrReleaseTag,
            OpenApiSpecId = command.OpenApiSpecId,
            HomepageId = command.HomepageId,
            ChangelogId = command.ChangelogId,
            IsBeta = command.IsBeta,
            IsHidden = command.IsHidden,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }

    private async Task UpdateFiles(string apiVersionId, FileItem[] fileItems)
    {
        var commandFileIds = fileItems.Select(x => x.FileId).ToList();
        var inDb  = await SafeDbExecuteAsync(async () => await dbContext.ApiVersionFiles.Where(x => x.ApiVersionId == apiVersionId).ToListAsync());
        var existingFileIds = inDb.Select(y => y.FileId).ToList();
        List<ApiVersionFile> toRemove = new();
        List<ApiVersionFile> toUpdate = new();
        List<ApiVersionFile> toInsert = new();
        toRemove.AddRange(inDb.Where(x=>!commandFileIds.Contains(x.FileId)));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
            
        foreach (var fileItem in fileItems.Where(x=>existingFileIds.Contains(x.FileId)))
        {
           toInsert.Add(new ApiVersionFile
            {
                ApiVersionId = apiVersionId,
                FileId = fileItem.FileId,
                OrderId = fileItem.OrderId,
                CreatedBy = userId,
                CreatedDate = timestamp,
            });
        }
        
        foreach (var fileItem in fileItems.Where(x=>existingFileIds.Contains(x.FileId)))
        {
            var apiVersionFile = inDb.First(x => x.FileId == fileItem.FileId);
            if (apiVersionFile.OrderId != fileItem.OrderId)
            {
                toRemove.Add(apiVersionFile);
                toInsert.Add(apiVersionFile with
                {
                    OrderId = fileItem.OrderId,
                    CreatedBy = userId,
                    CreatedDate = timestamp
                });
            }
        }
        
        foreach (var apiVersionFile in toRemove)
        {
            var success = await SafeDbExecuteAsync(async () =>
            {
                dbContext.ApiVersionFiles.Remove(apiVersionFile);
                var result = await dbContext.SaveChangesAsync();
                return result == 1;
            });
            if (!success)
                throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionFile");    
        }
        
        foreach (var apiVersionFile in toInsert)
        {
            var success = await SafeDbExecuteAsync(async () =>
            {
                dbContext.ApiVersionFiles.Add(apiVersionFile);
                var result = await dbContext.SaveChangesAsync();
                return result == 1;
            });
            if (!success)
                throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionFile");    
        }
    }
    private async Task UpdateGuideVersions(string apiVersionId, GuideVersionItem[] guideVersionItems)
    {
        var commandIds = guideVersionItems.Select(x => x.GuideVersionId).ToList();
        var inDb  = await SafeDbExecuteAsync(async () => await dbContext.ApiVersionGuideVersions.Where(x => x.ApiVersionId == apiVersionId).ToListAsync());
        var existingIds = inDb.Select(y => y.GuideVersionId).ToList();
        List<ApiVersionGuideVersion> toRemove = new();
        List<ApiVersionGuideVersion> toUpdate = new();
        List<ApiVersionGuideVersion> toInsert = new();
        toRemove.AddRange(inDb.Where(x=>!commandIds.Contains(x.GuideVersionId)));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
            
        foreach (var guideVersionItem in guideVersionItems.Where(x=>existingIds.Contains(x.GuideVersionId)))
        {
           toInsert.Add(new ApiVersionGuideVersion
            {
                ApiVersionId = apiVersionId,
                GuideVersionId = guideVersionItem.GuideVersionId,
                OrderId = guideVersionItem.OrderId,
                CreatedBy = userId,
                CreatedDate = timestamp,
            });
        }
        
        foreach (var guideVersionItem in guideVersionItems.Where(x=>existingIds.Contains(x.GuideVersionId)))
        {
            var apiVersionGuideVersion = inDb.First(x => x.GuideVersionId == guideVersionItem.GuideVersionId);
            if (apiVersionGuideVersion.OrderId != guideVersionItem.OrderId)
            {
                toRemove.Add(apiVersionGuideVersion);
                toInsert.Add(apiVersionGuideVersion with
                {
                    OrderId = guideVersionItem.OrderId,
                    CreatedBy = userId,
                    CreatedDate = timestamp
                });
            }
        }
        
        foreach (var apiVersionGuideVersion in toRemove)
        {
            var success = await SafeDbExecuteAsync(async () =>
            {
                dbContext.ApiVersionGuideVersions.Remove(apiVersionGuideVersion);
                var result = await dbContext.SaveChangesAsync();
                return result == 1;
            });
            if (!success)
                throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionFile");    
        }
        
        foreach (var apiVersionGuideVersion in toInsert)
        {
            var success = await SafeDbExecuteAsync(async () =>
            {
                dbContext.ApiVersionGuideVersions.Add(apiVersionGuideVersion);
                var result = await dbContext.SaveChangesAsync();
                return result == 1;
            });
            if (!success)
                throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionFile");    
        }
    }
}