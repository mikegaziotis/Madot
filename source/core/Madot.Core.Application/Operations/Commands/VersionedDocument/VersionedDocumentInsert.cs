using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Madot.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands;

public record VersionedDocumentInsertCommand: ICommand
{
    public required string ApiId { get; init; }

    public required VersionedDocumentType DocumentType { get; init; }

    public int Iteration { get; init; }

    public required string Data { get; init; }
}

public class VersionedDocumentInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider) :ICommandHandler<VersionedDocumentInsertCommand, Domain.Models.VersionedDocument, string>
{
    private int newIteration; 
    public override async Task<string> Handle(VersionedDocumentInsertCommand command)
    {
        if (!await SafeDbExecuteAsync(async () => await dbContext.Apis.Where(x => x.Id == command.ApiId).AnyAsync()))
            throw new EntityNotFoundException("Not Api entity exists for the provided ApiId");
        
        if (await SafeDbExecuteAsync(async () => await dbContext.VersionedDocuments.Where(x =>
                            x.ApiId == command.ApiId && x.DocumentType == command.DocumentType &&
                            x.Iteration == command.Iteration).AnyAsync()))
            throw new EntityConflictException("A VersionedDocument of the same DocumentType and Iteration already exists");

        newIteration = (await SafeDbExecuteAsync(async () =>
            await dbContext.VersionedDocuments
                .Where(x => x.ApiId == command.ApiId && x.DocumentType == command.DocumentType)
                .OrderByDescending(x => x.Iteration).FirstOrDefaultAsync()))?.Iteration ?? 1;
        
        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.VersionedDocuments.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new VersionedDocument entity");
        
        return newEntity.Id;
    }

    public override Domain.Models.VersionedDocument GetEntity(VersionedDocumentInsertCommand command, Domain.Models.VersionedDocument? existingEntity=null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();

        return new Domain.Models.VersionedDocument
        {
            Id = keyProvider.GenerateRandomKey(),
            DocumentType = command.DocumentType,
            ApiId = command.ApiId,
            Data = command.Data,
            Iteration = newIteration,
            CreatedBy = userId,
            CreatedDate = timestamp,
            LastModifiedBy = userId,
            LastModifiedDate = timestamp
        };
    }
}