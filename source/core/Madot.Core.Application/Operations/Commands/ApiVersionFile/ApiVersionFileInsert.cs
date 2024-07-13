using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands.ApiVersionFile;

public record ApiVersionFileInsertCommand: ICommand
{
    public required string ApiVersionId { get; init; }

    public required string FileId { get; init; }

    public int OrderId { get; init; }

}

public class ApiVersionFileInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider) :ICommandHandler<ApiVersionFileInsertCommand, Domain.Models.ApiVersionFile>
{
    public override async Task Handle(ApiVersionFileInsertCommand command)
    {
        if (!await SafeDbExecuteAsync(async () => await dbContext.ApiVersions.Where(x => x.Id == command.ApiVersionId).AnyAsync()))
            throw new EntityNotFoundException("No ApiVersion entity exists for the provided ApiVersionId");
        if (!await SafeDbExecuteAsync(async () => await dbContext.Files.Where(x => x.Id == command.FileId).AnyAsync()))
            throw new EntityNotFoundException("No File entity exists for the provided FileId");
        
        if (!await SafeDbExecuteAsync(async () => await dbContext.ApiVersionFiles.Where(x =>
                            x.ApiVersionId == command.ApiVersionId && x.FileId == command.FileId).AnyAsync()))
            throw new EntityConflictException("An entity with the same ApiVersionId and FileId already exists");

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersionFiles.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new ApiVersionFile entity");
        
    }

    public override Domain.Models.ApiVersionFile GetEntity(ApiVersionFileInsertCommand command, Domain.Models.ApiVersionFile? existingEntity=null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();

        return new Domain.Models.ApiVersionFile
        {
            ApiVersionId = command.ApiVersionId,
            FileId = command.FileId,
            OrderId = command.OrderId,
            CreatedBy = userId,
            CreatedDate = timestamp,
        };
    }
}