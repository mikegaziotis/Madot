namespace Badop.Interface.API.Models.Requests;

public record ApiVersionRequest(string ApiName, string Key)
{
    public string? OpenApiSpec { get; set; }
    public string? Markdown { get; set; }
    public bool? IsPreview { get; set; }
    public bool? IsAvailable { get; set; }
}