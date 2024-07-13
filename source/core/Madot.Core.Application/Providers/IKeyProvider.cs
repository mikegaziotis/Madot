namespace Madot.Core.Application.Providers;

public interface IKeyProvider
{
    public string GenerateRandomKey();
}

public class KeyProvider : IKeyProvider
{
    public string GenerateRandomKey()
    {
        return GenerateRandomString(10);
    }

    private string GenerateRandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}