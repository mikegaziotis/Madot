using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class ApiVersionFile
{
    public string ApiVersionId { get; set; } = null!;

    public string FileId { get; set; } = null!;

    public int OrderId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ApiVersion ApiVersion { get; set; } = null!;

    public virtual File File { get; set; } = null!;
}
