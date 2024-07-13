namespace Madot.Core.Application.Providers;


public interface IDateTimeProvider
{
    DateTime GetUtcNow();
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow() => DateTime.UtcNow;
}

