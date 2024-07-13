using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.Api;

public record ApiVersionGuideVersionDeleteCommand: ICommand
{
    public required string Id { get; init; }
}

public class ApiVersionGuideVersionDeleteCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiVersionGuideVersionDeleteCommand,Domain.Models.ApiVersionGuideVersion>
{
    public override async Task Handle(ApiVersionGuideVersionDeleteCommand command)
    {
        var apiVersionGuideVersion = await SafeDbExecuteAsync(async () => await dbContext.ApiVersionGuideVersions.FindAsync(command.Id));
        if (apiVersionGuideVersion is null)
            throw new EntityNotFoundException("An ApiVersionGuideVersion with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersionGuideVersions.Remove(apiVersionGuideVersion);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionGuideVersion");
    }

    public override Domain.Models.ApiVersionGuideVersion GetEntity(ApiVersionGuideVersionDeleteCommand command, Domain.Models.ApiVersionGuideVersion? existingEntity)
    {
        return null!;
    }
}