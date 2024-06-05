using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class VersionedDocument
{
    public string Id { get; set; } = null!;

    public string ApiId { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public int Iteration { get; set; }

    public string Data { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual Api Api { get; set; } = null!;

    public virtual ICollection<ApiVersion> ApiVersionChangelogs { get; set; } = new List<ApiVersion>();

    public virtual ICollection<ApiVersion> ApiVersionHomepages { get; set; } = new List<ApiVersion>();

    public virtual ICollection<ApiVersion> ApiVersionOpenApiSpecs { get; set; } = new List<ApiVersion>();
}
