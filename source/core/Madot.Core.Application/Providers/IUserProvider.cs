namespace Madot.Core.Application.Providers;

public interface IUserProvider
{
    User GetUser();
}

public record User(string UserId, string? Username);