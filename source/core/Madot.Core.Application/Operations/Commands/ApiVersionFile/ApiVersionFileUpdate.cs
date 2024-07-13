using Madot.Core.Application.Exceptions;
using Madot.Core.Application.Providers;

namespace Madot.Core.Application.Operations.Commands.Api;

public record ApiVersionFileDeleteCommand: ICommand
{
    public required string Id { get; init; }
}

public class ApiVersionFileDeleteCommandHandler(
    MadotDbContext dbContext,
    IUserProvider userProvider,
    IDateTimeProvider dateTimeProvider): ICommandHandler<ApiVersionFileDeleteCommand,Domain.Models.ApiVersionFile>
{
    public override async Task Handle(ApiVersionFileDeleteCommand command)
    {
        var apiVersionFile = await SafeDbExecuteAsync(async () => await dbContext.ApiVersionFiles.FindAsync(command.Id));
        if (apiVersionFile is null)
            throw new EntityNotFoundException("An ApiVersionFile with that Id does not exist");
        
        var success = await SafeDbExecuteAsync(async () =>
        {
            dbContext.ApiVersionFiles.Remove(apiVersionFile);
            var result = await dbContext.SaveChangesAsync();
            return result == 1;
        });
        if (!success)
            throw new UnexpectedDatabaseResultException("Failed to remove ApiVersionFile");
    }

    public override Domain.Models.ApiVersionFile GetEntity(ApiVersionFileDeleteCommand command, Domain.Models.ApiVersionFile? existingEntity)
    {
        return null!;
    }
}