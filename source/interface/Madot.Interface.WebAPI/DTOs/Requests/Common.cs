using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Madot.Interface.WebAPI.Requests;

public record JsonPatchOperation
{
    [AllowedValues("replace","add","remove","copy","move")]
    public string op { get; set; }
    public string path { get; set; }
    public JsonElement value { get; set; }
}

public record StringIdCreated(string Id);
public record IntIdCreated(int Id);
