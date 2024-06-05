using Badop.Core.Domain.Models;

namespace Badop.Core.Domain.ShortTypes;

public record VersionedDocumentShort(string Id, int Iteration, DateTime LastUpdated)
{
    public VersionedDocumentShort(VersionedDocument vd) : this(vd.Id, vd.Iteration, vd.LastModifiedDate)
    {
    }
}

public record ApiVersionShort(string ApiVersionId, string Version)
{
    public ApiVersionShort(ApiVersion apiVersion): this(apiVersion.Id,$"{apiVersion.MajorVersion}.{apiVersion.MinorVersion}")
    {
        
    } 
}

public record GuideVersionShort(string Id, string Title)
{
    public GuideVersionShort(GuideVersion v):this(v.Id,v.Guide!.Title)
    {
        
    }
}