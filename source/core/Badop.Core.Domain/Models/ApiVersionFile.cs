namespace Badop.Core.Domain.Models;

public record ApiVersionFile
{
    public required string ApiVersionId { get; init; }

    public required string FileId { get; init; }

    public int OrderId { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public virtual ApiVersion ApiVersion { get; init; } = null!;

    public virtual File File { get; init; } = null!;
}
