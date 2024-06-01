namespace Badop.Core.Domain.Entities;

public record ApiVersion: ApiVersionShort
{ 
    public required string Id { get; init; }
    public string? BuildOrReleaseReference { get; set; }
    public required string OpenApiSpecId { get; set; }
    public required string DocumentationId { get; set; }
    public string? ChangeLogId { get; set; }
}

public record ApiVersionShort
{
    public required string ApiName { get; init; }
    public required int MajorVersion { get; init; }
    public required int MinorVersion { get; init; }

    public bool IsPreview { get; set; } = true;
    public bool IsAvailable { get; set; } = true;

    public string GenerateId()
    {
        return $"{ApiName}:{MajorVersion}.{MinorVersion}";
    }

}