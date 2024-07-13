using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Madot.Shell.API.Requests;

public class JsonPatchOperation
{
    [AllowedValues("replace","add","remove","copy","move")]
    public string op { get; set; }
    public string path { get; set; }
    public JsonElement value { get; set; }
}