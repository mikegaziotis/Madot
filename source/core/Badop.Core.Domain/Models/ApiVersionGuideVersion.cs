using System;
using System.Collections.Generic;

namespace Badop.Core.Domain.Models;

public record ApiVersionGuideVersion
{
    public required string ApiVersionId { get; init; }

    public required string GuideVersionId { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public virtual ApiVersion? ApiVersion { get; init; }

    public virtual GuideVersion? GuideVersion { get; init; }
}
