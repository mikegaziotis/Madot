using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands;

public record AppCommonPageInsertCommand: ICommand
{
    public required string Title { get; init; }
    public required string Data { get; init; }
    public int OrderId { get; init; }
}

public class AppCommonPageInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<AppCommonPageInsertCommand,Domain.Models.AppCommonPage,int>
{
    public override async Task<int> Handle(AppCommonPageInsertCommand command)
    {
        var alreadyExistsTitle = await SafeDbExecuteAsync(async()=>await dbContext.AppCommonPages.AnyAsync(x => x.Title==command.Title));
        if (alreadyExistsTitle)
            throw new EntityConflictException("AppCommonPage with the same Title already exists. Title must be unique");

        var alreadyExistsOrder = await SafeDbExecuteAsync(async () => await dbContext.AppCommonPages.AnyAsync(x => x.OrderId == command.OrderId && !x.IsDeleted));
        if (alreadyExistsOrder)
            throw new EntityConflictException("AppCommonPage with the same OrderId already exists. OrderId must be unique.");

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async() =>
        {
            dbContext.AppCommonPages.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new AppCommonPage entity");
        
        return newEntity.Id;
    }

    public override Domain.Models.AppCommonPage GetEntity(AppCommonPageInsertCommand command, Domain.Models.AppCommonPage? existingEntity =null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return new Domain.Models.AppCommonPage
        {
            Title = command.Title,
            Data = command.Data,
            OrderId = command.OrderId,
            IsDeleted = false,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}