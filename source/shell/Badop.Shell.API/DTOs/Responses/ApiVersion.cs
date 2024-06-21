namespace Badop.Shell.API.DTOs.Responses;

public record ApiVersion
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public int MajorVersion { get; init; }

    public int MinorVersion { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }

    public bool IsHidden { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }
}