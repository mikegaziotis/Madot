using System.Text.Json.Serialization;

namespace Madot.Interface.CLI.Responses;

public record CreatedResponse([property: JsonPropertyName("id")]string Id);

public record FailureResponse([property: JsonPropertyName("success")]string Success);

public record ErrorResponse([property: JsonPropertyName("error")]string Error);
public record NotFoundResponse([property: JsonPropertyName("notFound")]string NotFound);

