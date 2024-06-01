using Badop.Interface.API.Models.Common;
namespace Badop.Interface.API.Models.Responses;

public record ApiListResponse (int Total,  IEnumerable<Api>? ApiList );