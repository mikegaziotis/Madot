namespace Badop.Interface.API.Models.Common;

public record Api (string Name, string DisplayName)
{
    public bool IsInternal { get; set; } = false;
    public bool IsBeta { get; set; } = false;
    public bool IsAvailable { get; set; } = true;
    public int OrderId { get; set; } = 1;
}