using System;
using System.Collections.Generic;
using Madot.Core.Domain.Enums;
using OperatingSystem = Madot.Core.Domain.Enums.OperatingSystem;

namespace Madot.Core.Domain.Models;

public record FileLink:IModel
{
    public required string FileId { get; init; }

    public required OperatingSystem OperatingSystem { get; init; }

    public required ChipArchitecture ChipArchitecture { get; init; }

    public required string DownloadUrl { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual File File { get; init; } = null!;
}
