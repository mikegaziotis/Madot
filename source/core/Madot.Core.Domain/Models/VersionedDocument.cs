﻿using System;
using System.Collections.Generic;
using Madot.Core.Domain.Enums;

namespace Madot.Core.Domain.Models;

public record VersionedDocument:IModel
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public required VersionedDocumentType DocumentType { get; init; }

    public int Iteration { get; init; }

    public required string Data { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual Api? Api { get; init; }

    public virtual ICollection<ApiVersion> ApiVersionChangelogs { get; init; } = new List<ApiVersion>();

    public virtual ICollection<ApiVersion> ApiVersionHomepages { get; init; } = new List<ApiVersion>();

    public virtual ICollection<ApiVersion> ApiVersionOpenApiSpecs { get; init; } = new List<ApiVersion>();
}
