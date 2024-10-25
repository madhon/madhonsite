namespace Romulus.Web.Infrastructure;

using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.Never,
    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
    GenerationMode = JsonSourceGenerationMode.Default)
]
[JsonSerializable(typeof(BuildInfo))]
internal sealed partial class AppJsonSerializerContext : JsonSerializerContext;
