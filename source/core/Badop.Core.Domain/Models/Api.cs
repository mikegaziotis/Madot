using System;
using System.Collections.Generic;

namespace Badop.Core.Domain.Models;

public record Api
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
 
    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual ICollection<ApiVersion> ApiVersions { get; init; } = new List<ApiVersion>();

    public virtual ICollection<File> Files { get; init; } = new List<File>();

    public virtual ICollection<Guide> Guides { get; init; } = new List<Guide>();

    public virtual ICollection<VersionedDocument> VersionedDocuments { get; init; } = new List<VersionedDocument>();
}
