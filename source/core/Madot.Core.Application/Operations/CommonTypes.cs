using Madot.Core.Domain.Enums;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Core.Application.Operations;

public record FileLinkItem
{
    public required OperatingSystem OperatingSystem { get; init; }

    public required ChipArchitecture ChipArchitecture { get; init; }

    public required string DownloadUrl { get; init; }
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
