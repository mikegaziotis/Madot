using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.Guide;

public record GuideUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public int ProvisionalOrderId { get; init; }
    
    public bool IsDeleted { get; init; }
}

public class GuideUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<GuideUpdateCommand,Domain.Models.Guide>
{
    public override async Task Handle(GuideUpdateCommand command)
    {
        var guide = await SafeDbExecuteAsync(async () => await dbContext.Guides.FindAsync(command.Id));
        if (guide is null)
            throw new EntityNotFoundException("An Guide with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.Guides.Update(GetEntity(command, guide));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update Guide entity");
    }

    public override Domain.Models.Guide GetEntity(GuideUpdateCommand command, Domain.Models.Guide? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            ProvisionalOrderId = command.ProvisionalOrderId,
            IsDeleted = command.IsDeleted,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}