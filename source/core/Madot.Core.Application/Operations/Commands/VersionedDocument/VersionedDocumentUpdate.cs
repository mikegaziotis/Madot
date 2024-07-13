using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Madot.Core.Domain.Enums;

namespace Madot.Core.Application.Operations.Commands.Api;

public record VersionedDocumentUpdateCommand: ICommand
{
    public required string Id { get; init; }

    public required string Data { get; init; }
}

public class VersionedDocumentUpdateCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<VersionedDocumentUpdateCommand,Domain.Models.VersionedDocument>
{
    public override async Task Handle(VersionedDocumentUpdateCommand command)
    {
        var versionedDocument = await SafeDbExecuteAsync(async () => await dbContext.VersionedDocuments.FindAsync(command.Id));
        if (versionedDocument is null)
            throw new EntityNotFoundException("An VersionedDocument with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.VersionedDocuments.Update(GetEntity(command, versionedDocument));
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to update Api entity");
    }

    public override Domain.Models.VersionedDocument GetEntity(VersionedDocumentUpdateCommand command, Domain.Models.VersionedDocument? existingEntity)
    {
        if (existingEntity is null)
            throw new ArgumentNullException(nameof(existingEntity));

        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();
        
        return existingEntity with
        {
            Data = command.Data,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}