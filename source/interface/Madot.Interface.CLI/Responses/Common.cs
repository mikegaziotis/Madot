using System.Text.Json.Serialization;

namespace Madot.Interface.CLI.Responses;

public record CreatedResponse([property: JsonPropertyName("created")]string Created);
public record ErrorResponse([property: JsonPropertyName("error")]string Error);
public record NotFoundResponse([property: JsonPropertyName("notFound")]string NotFound);

