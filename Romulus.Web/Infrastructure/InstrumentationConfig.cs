namespace Romulus.Web.Infrastructure;

public static class InstrumentationConfig 
{
    private const string serviceName = "Romulus.Web";

    public static ActivitySource ActivitySource { get; set; } = new(ServiceName);

    public static string ServiceName => serviceName;
}
