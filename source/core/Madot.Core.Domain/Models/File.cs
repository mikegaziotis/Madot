using System;
using System.Collections.Generic;

namespace Madot.Core.Domain.Models;

public record File:IModel
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

    public virtual Api Api { get; init; } = null!;
    public virtual ICollection<ApiVersionFile> ApiVersionFiles { get; init; } = new List<ApiVersionFile>();

    public virtual ICollection<FileLink> FileLinks { get; init; } = new List<FileLink>();
}
