using System;
using System.Collections.Generic;

namespace Badop.Core.Domain.Models;

public record Guide
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public required string Title { get; init; }

    public bool IsDeleted { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual Api? Api { get; init; }

    public virtual ICollection<GuideVersion> GuideVersions { get; init; } = new List<GuideVersion>();
}
