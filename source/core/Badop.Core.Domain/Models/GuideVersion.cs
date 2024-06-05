using System;
using System.Collections.Generic;

namespace Badop.Core.Domain.Models;

public record GuideVersion
{
    public required string Id { get; init; }

    public required string GuideId { get; init; }

    public int Iteration { get; init; }

    public required string Data { get; init; }

    public bool IsDeleted { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual ICollection<ApiVersionGuideVersion> ApiVersionGuideVersions { get; init; } = new List<ApiVersionGuideVersion>();

    public virtual Guide? Guide { get; init; }
}
