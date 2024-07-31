using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands;

public record GuideVersionInsertCommand: ICommand
{
    public required string GuideId { get; init; }

    public required string Data { get; init; }

}

public class GuideVersionInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<GuideVersionInsertCommand,Domain.Models.GuideVersion,string>
{
    private int _iteration = 1;
    
    public override async Task<string> Handle(GuideVersionInsertCommand command)
    {
        _ = await SafeDbExecuteAsync(async () => await dbContext.Guides.FindAsync(command.GuideId)) ??
            throw new EntityNotFoundException("The provided Guide Id was not found");

        _iteration = (await SafeDbExecuteAsync(async () =>
            await dbContext.GuideVersions.Where(x => x.GuideId == command.GuideId).OrderByDescending(x => x.Iteration)
                .FirstOrDefaultAsync()))?.Iteration??0 + 1;
        
        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async() =>
        {
            dbContext.GuideVersions.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new GuideVersion entity");

        return newEntity.Id;
    }

    public override Domain.Models.GuideVersion GetEntity(GuideVersionInsertCommand command, Domain.Models.GuideVersion? existingEntity =null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        var id = keyProvider.GenerateRandomKey();
        
        return new Domain.Models.GuideVersion
        {
            Id = id,
            GuideId = command.GuideId,
            Data = command.Data,
            IsDeleted = false,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}