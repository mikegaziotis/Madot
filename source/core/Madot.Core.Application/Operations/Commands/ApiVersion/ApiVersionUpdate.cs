using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.Api;

public record ApiVersionUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }

    public bool IsHidden { get; init; }
}

public class ApiVersionUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiVersionUpdateCommand,Domain.Models.ApiVersion>
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
    }

    public override Domain.Models.ApiVersion GetEntity(ApiVersionUpdateCommand command, Domain.Models.ApiVersion? existingEntity)
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
}