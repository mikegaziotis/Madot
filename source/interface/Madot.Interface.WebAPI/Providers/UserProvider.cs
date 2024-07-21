using Madot.Core.Application.Providers;

namespace Madot.Interface.WebAPI.Providers;

public class UserProvider: IUserProvider
{
    public User GetUser()
    {
        return new User("demo-user", "Demo User");
    }
}