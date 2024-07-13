using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands.ApiVersion;

public record ApiVersionInsertCommand: ICommand
{
    public required string ApiId { get; init; }

    public int MajorVersion { get; init; }

    public int MinorVersion { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }

    public bool IsHidden { get; init; }
}

public class ApiVersionInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider) :ICommandHandler<ApiVersionInsertCommand, Domain.Models.ApiVersion, string>
{
    public override async Task<string> Handle(ApiVersionInsertCommand command)
    {
        if (!await SafeDbExecuteAsync(async () => await dbContext.Apis.Where(x => x.Id == command.ApiId).AnyAsync()))
            throw new EntityNotFoundException("Not Api entity exists for the provided ApiId");
            
        
        if (!await SafeDbExecuteAsync(async () => await dbContext.ApiVersions.Where(x =>
                            x.ApiId == command.ApiId && x.MajorVersion == command.MajorVersion &&
                            x.MinorVersion == command.MinorVersion).AnyAsync()))
            throw new EntityConflictException("An ApiVersion with Major.Minor number already exists for that Api Id");

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersions.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new ApiVersion entity");
        
        return newEntity.Id;
    }

    public override Domain.Models.ApiVersion GetEntity(ApiVersionInsertCommand command, Domain.Models.ApiVersion? existingEntity=null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();

        return new Domain.Models.ApiVersion
        {
            Id = keyProvider.GenerateRandomKey(),
            ApiId = command.ApiId,
            MajorVersion = command.MajorVersion,
            MinorVersion = command.MinorVersion,
            BuildOrReleaseTag = command.BuildOrReleaseTag,
            OpenApiSpecId = command.OpenApiSpecId,
            HomepageId = command.HomepageId,
            ChangelogId = command.ChangelogId,
            IsBeta = command.IsBeta,
            IsHidden = command.IsHidden,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}