using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Commands.ApiVersionGuideVersion;

public record ApiVersionGuideVersionInsertCommand: ICommand
{
    public required string ApiVersionId { get; init; }

    public required string GuideVersionId { get; init; }

    public int OrderId { get; init; }

}

public class ApiVersionGuideVersionInsertCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IKeyProvider keyProvider,
    IDateTimeProvider dateTimeProvider) :ICommandHandler<ApiVersionGuideVersionInsertCommand, Domain.Models.ApiVersionGuideVersion>
{
    public override async Task Handle(ApiVersionGuideVersionInsertCommand command)
    {
        if (!await SafeDbExecuteAsync(async () => await dbContext.ApiVersions.Where(x => x.Id == command.ApiVersionId).AnyAsync()))
            throw new EntityNotFoundException("No ApiVersion entity exists for the provided ApiVersionId");
        if (!await SafeDbExecuteAsync(async () => await dbContext.GuideVersions.Where(x => x.Id == command.GuideVersionId).AnyAsync()))
            throw new EntityNotFoundException("No GuideVersion entity exists for the provided GuideVersionId");
        
        if (!await SafeDbExecuteAsync(async () => await dbContext.ApiVersionGuideVersions.Where(x =>
                            x.ApiVersionId == command.ApiVersionId && x.GuideVersionId == command.GuideVersionId).AnyAsync()))
            throw new EntityConflictException("An entity with the same ApiVersionId and GuideVersionId already exists");

        var newEntity = GetEntity(command);
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersionGuideVersions.Add(newEntity);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to add new ApiVersionGuideVersion entity");
        
    }

    public override Domain.Models.ApiVersionGuideVersion GetEntity(ApiVersionGuideVersionInsertCommand command, Domain.Models.ApiVersionGuideVersion? existingEntity=null)
    {
        var userId = userProvider.GetUser().UserId;
        var timestamp = dateTimeProvider.GetUtcNow();

        return new Domain.Models.ApiVersionGuideVersion
        {
            ApiVersionId = command.ApiVersionId,
            GuideVersionId = command.GuideVersionId,
            OrderId = command.OrderId,
            CreatedBy = userId,
            CreatedDate = timestamp,
        };
    }
}