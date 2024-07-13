using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.Api;

public record ApiUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
 
    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }
}

public class ApiUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiUpdateCommand,Domain.Models.Api>
{
    public override async Task Handle(ApiUpdateCommand command)
    {
        var api = await SafeDbExecuteAsync(async () => await dbContext.Apis.FindAsync(command.Id));
        if (api is null)
            throw new EntityNotFoundException("An Api with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.Apis.Update(GetEntity(command, api));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update Api entity");
    }

    public override Domain.Models.Api GetEntity(ApiUpdateCommand command, Domain.Models.Api? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));
        
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            DisplayName = command.DisplayName,
            BaseUrlPath = command.BaseUrlPath,
            IsInternal = command.IsInternal,
            IsPreview = command.IsPreview,
            IsHidden = command.IsHidden,
            OrderId = command.OrderId,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}