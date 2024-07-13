using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.GuideVersion;

public record GuideVersionUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public string Data { get; init; }
    
    public bool IsDeleted { get; init; }
}

public class GuideVersionUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<GuideVersionUpdateCommand,Domain.Models.GuideVersion>
{
    public override async Task Handle(GuideVersionUpdateCommand command)
    {
        var guideVersion = await SafeDbExecuteAsync(async () => await dbContext.GuideVersions.FindAsync(command.Id));
        if (guideVersion is null)
            throw new EntityNotFoundException("An GuideVersion with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.GuideVersions.Update(GetEntity(command, guideVersion));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update GuideVersion entity");
    }

    public override Domain.Models.GuideVersion GetEntity(GuideVersionUpdateCommand command, Domain.Models.GuideVersion? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            Data = command.Data,
            IsDeleted = command.IsDeleted,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}