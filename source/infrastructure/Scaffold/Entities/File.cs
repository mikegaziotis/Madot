using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class File
{
    public string Id { get; set; } = null!;

    public string ApiId { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual Api Api { get; set; } = null!;

    public virtual ICollection<ApiVersionFile> ApiVersionFiles { get; set; } = new List<ApiVersionFile>();

    public virtual ICollection<FileLink> FileLinks { get; set; } = new List<FileLink>();
}
