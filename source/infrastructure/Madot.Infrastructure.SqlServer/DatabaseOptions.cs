namespace Madot.Infrastructure.SqlServer;

public class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";
    public required string SqlConnectionString { get; set; }
}