using Badop.Core.Domain.Enums;

namespace Badop.Core.Domain.Entities;

public record ChangeLog
{
    public required string Id { get; init; }
    public required string ApiName { get; init; }
    public string? ApiVersion { get; init; }
    public FileTypeExtension FileTypeExtension { get; init; }
    public required string Data { get; init; }
 };
