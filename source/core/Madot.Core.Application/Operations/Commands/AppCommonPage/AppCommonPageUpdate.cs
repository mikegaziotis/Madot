using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands.AppCommonPage;

public record AppCommonPageUpdateCommand: ICommand
{
    public int Id { get; init; }
    public required string Data { get; init; }
    public int OrderId { get; init; }
    public bool IsDeleted { get; init; }
}

public class AppCommonPageUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<AppCommonPageUpdateCommand,Domain.Models.AppCommonPage>
{
    public override async Task Handle(AppCommonPageUpdateCommand command)
    {
        var AppCommonPage = await SafeDbExecuteAsync(async () => await dbContext.AppCommonPages.FindAsync(command.Id));
        if (AppCommonPage is null)
            throw new EntityNotFoundException("An AppCommonPage with that Id does not exist");

        if (!command.IsDeleted)
        {
            if(await SafeDbExecuteAsync(async () => 
                   await dbContext.AppCommonPages.AnyAsync(x=>x.OrderId == command.OrderId && x.Id!=command.Id)));
            {
                throw new InvalidArgumentException("A different AppCommonPage with the same orderId already exists. OrderId must be unique");
            }
        }
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.AppCommonPages.Update(GetEntity(command, AppCommonPage));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update AppCommonPage entity");
    }

    public override Domain.Models.AppCommonPage GetEntity(AppCommonPageUpdateCommand command, Domain.Models.AppCommonPage? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            Data = command.Data,
            OrderId = command.OrderId,
            IsDeleted = command.IsDeleted,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}