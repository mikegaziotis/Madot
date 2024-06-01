using Badop.Core.Domain.Enums;

namespace Badop.Core.Domain.Entities;

public record VersionedDocument
{
    public required string Id { get; init; }
    public required string ApiName { get; init; }
    public string? ApiVersion { get; init; }
    public VersionedDocumentType DocumentType { get; init; }
    public int Version { get; set; }
    public FileTypeExtension FileTypeExtension { get; init; }
    public required string Data { get; set; }
    public bool IsLatest { get; set; }
}