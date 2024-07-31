namespace Madot.Interface.WebAPI.DTOs.Responses;

public abstract record VersionedDocument
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public int Iteration { get; init; }

    public required string Data { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }    
}

public record Homepage : VersionedDocument;
public record Changelog : VersionedDocument;
public record OpenApiSpec : VersionedDocument;
public abstract record VersionedDocumentShort(string Id, int Iteration, DateTime LastUpdated);
public record HomepageShort(string Id, int Iteration, DateTime LastUpdated): VersionedDocumentShort(Id,Iteration,LastUpdated);
public record ChangelogShort(string Id, int Iteration, DateTime LastUpdated): VersionedDocumentShort(Id,Iteration,LastUpdated);
public record OpenApiSpecShort(string Id, int Iteration, DateTime LastUpdated): VersionedDocumentShort(Id,Iteration,LastUpdated);