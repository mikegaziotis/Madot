using Madot.Core.Domain.Enums;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Interface.WebAPI.DTOs.Requests;

public class FileInsertCommand
{
    public required string ApiId { get; init; }
    public required string DisplayName { get; init; }
    
    public string? Description { get; init; }

    public string? ImageUrl { get; init; }

    public required FileLinkItem[] FileLinks { get; init; }
    
}

public record FileUpdateCommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
    
    public string? Description { get; init; }

    public string? ImageUrl { get; init; }
    public bool IsDeleted { get; init; }

    public required FileLinkItem[] FileLinks { get; init; }
   
}

public record FileLinkItem
{
    public required OperatingSystem OperatingSystem { get; init; }

    public required ChipArchitecture ChipArchitecture { get; init; }

    public required string DownloadUrl { get; init; }
}