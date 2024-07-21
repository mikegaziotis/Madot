namespace Madot.Interface.WebAPI.DTOs.Requests;

public record ApiVersionInsertCommand
{
    public required string ApiId { get; init; }

    public int MajorVersion { get; init; }

    public int MinorVersion { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }
    
    public bool IsHidden { get; init; }

    public GuideVersionItem[] GuideVersionItems { get; init; } = [];
    
    public FileItem[] FileItems { get; init; } = [];
    
}


public record ApiVersionUpdateCommand
{
    public required string Id { get; init; }

    public string? BuildOrReleaseTag { get; init; }

    public required string OpenApiSpecId { get; init; }

    public string? HomepageId { get; init; }

    public string? ChangelogId { get; init; }

    public bool IsBeta { get; init; }

    public bool IsHidden { get; init; }

    public GuideVersionItem[] GuideVersionItems { get; init; } = [];
    
    public FileItem[] FileItems { get; init; } = [];
}

public record GuideVersionItem
{

    public required string GuideVersionId { get; init; }

    public int OrderId { get; init; }
}

public record FileItem
{
    public required string FileId { get; init; }

    public int OrderId { get; init; }
}
