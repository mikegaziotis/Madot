using Madot.Core.Domain.Enums;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Shell.API.DTOs.Responses;


public record File
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public required string DisplayName { get; init; }
    
    public string? Description { get; init; }
    
    public string? ImageUrl { get; init; }

    public bool IsDeleted { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }
    
    public virtual ICollection<FileLink> FileLinks { get; init; } = new List<FileLink>();
}

public record FileLink
{
    public required string FileId { get; init; }

    public required OperatingSystem OperatingSystem { get; init; }

    public required ChipArchitecture ChipArchitecture { get; init; }

    public required string DownloadUrl { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }
}