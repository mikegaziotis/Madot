using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands;

public record ApiInsertCommand: ICommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
 
    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }
}

public class ApiInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiInsertCommand,Domain.Models.Api>
{
    public override async Task Handle(ApiInsertCommand command)
    {
        var alreadyExists = await SafeDbExecuteAsync(async()=>await dbContext.Apis.AnyAsync(x => x.Id == command.Id));
        if (alreadyExists)
        {
            throw new EntityConflictException("Api with the same Id already exists.");
        }
        var success = await SafeDbExecuteAsync(async() =>
        {
            dbContext.Apis.Add(GetEntity(command));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new Api entity");
    }

    public override Domain.Models.Api GetEntity(ApiInsertCommand command, Domain.Models.Api? existingEntity =null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        return new Domain.Models.Api
        {
            Id = command.Id,
            DisplayName = command.DisplayName,
            BaseUrlPath = command.BaseUrlPath,
            IsInternal = command.IsInternal,
            IsPreview = command.IsPreview,
            IsHidden = command.IsHidden,
            OrderId = command.OrderId,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}