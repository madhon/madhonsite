namespace Romulus.Web.Infrastructure;

using System.Text.Json.Serialization;

internal sealed class AzureFeatureFlagPayload
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}
