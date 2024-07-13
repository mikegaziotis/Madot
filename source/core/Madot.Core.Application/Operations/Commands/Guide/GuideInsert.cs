using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands.Guide;

public record GuideInsertCommand: ICommand
{
    
    public required string ApiId { get; init; }

    public required string Title { get; init; }

    public int ProvisionalOrderId { get; init; }
    
}

public class GuideInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<GuideInsertCommand,Domain.Models.Guide,string>
{
    public override async Task<string> Handle(GuideInsertCommand command)
    {
        _ = await SafeDbExecuteAsync(async () => await dbContext.Apis.FindAsync(command.ApiId)) ??
            throw new EntityNotFoundException("The provided Api Id was not found");
        
        var alreadyExists = await SafeDbExecuteAsync(async()=>await dbContext.Guides.AnyAsync(x => x.Title == command.Title 
            && x.ApiId == command.ApiId
            && !x.IsDeleted));
        if (alreadyExists)
        {
            throw new EntityConflictException("Guide with the same Title already exists for this Api Id.");
        }

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async() =>
        {
            dbContext.Guides.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new Guide entity");

        return newEntity.Id;
    }

    public override Domain.Models.Guide GetEntity(GuideInsertCommand command, Domain.Models.Guide? existingEntity =null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        var id = keyProvider.GenerateRandomKey();
        
        return new Domain.Models.Guide
        {
            Id = id,
            ApiId = command.ApiId,
            Title = command.Title,
            ProvisionalOrderId = command.ProvisionalOrderId,
            IsDeleted = false,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}