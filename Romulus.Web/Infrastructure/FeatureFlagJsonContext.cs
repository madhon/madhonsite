namespace Romulus.Web.Infrastructure;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(AzureFeatureFlagPayload))]
internal sealed partial class FeatureFlagJsonContext : JsonSerializerContext
{
}

