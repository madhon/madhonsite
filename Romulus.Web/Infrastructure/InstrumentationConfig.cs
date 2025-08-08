namespace Romulus.Web.Infrastructure;

internal static class InstrumentationConfig
{
    public static ActivitySource ActivitySource { get; set; } = new(ServiceName);

    public static string ServiceName => "Romulus.Web";
}
