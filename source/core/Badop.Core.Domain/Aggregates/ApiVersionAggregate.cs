using System.ComponentModel;
using Badop.Core.Domain.Entities;
using Badop.Core.Domain.Services;

namespace Badop.Core.Domain.Aggregates;

public record ApiVersionAggregate
{
    public required string Id { get; init; }
    public required string ApiName { get; init; }
    public int MajorVersion { get; init; }
    public int MinorVersion { get; init; }
    public bool IsPreview { get; init; }
    public bool IsAvailable { get; init; }
    public string? BuildOrReleaseReference { get; init; } 
    public required VersionedDocument OpenApiSpec { get; init; }
    public required VersionedDocument Documentation { get; init; } 
    public ChangeLog? ChangeLog { get; init; } 
}
