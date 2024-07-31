namespace Madot.Interface.WebAPI.DTOs.Requests;

public record VersionedDocumentInsertCommand
{
    public required string ApiId { get; init; }
    public required string Data { get; init; }
}

public record VersionedDocumentUpdateCommand
{
    public required string Id { get; init; }

    public required string Data { get; init; }
}

