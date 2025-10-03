namespace Romulus.Web.Infrastructure;

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

internal static class OpenTelemetryExtensions
{
    private const string HealthEndpointPath = "/healthz";
    private const string AlivenessEndpointPath = "/alive";

    public static IHostApplicationBuilder AddOpenTelemetry(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Logging.AddOpenTelemetry(o =>
        {
            o.IncludeFormattedMessage = true;
            o.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    tracing.SetSampler(new AlwaysOnSampler());
                }

                tracing
                    .AddSource(InstrumentationConfig.ActivitySource.Name)
                    .ConfigureResource(resource => resource.AddService(InstrumentationConfig.ServiceName))
                    .AddAspNetCoreInstrumentation(nci =>
                    {
                        nci.RecordException = true;
                        nci.Filter = httpContext =>
                            !(httpContext.Request.Path.StartsWithSegments(HealthEndpointPath,
                                  StringComparison.OrdinalIgnoreCase)
                              || httpContext.Request.Path.StartsWithSegments(AlivenessEndpointPath,
                                  StringComparison.OrdinalIgnoreCase));
                    });
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .ConfigureResource(resource => resource.AddService(InstrumentationConfig.ServiceName))
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddBuiltInMeters();
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        return builder;
    }

    private static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
    meterProviderBuilder.AddMeter(
        "Microsoft.AspNetCore.Hosting",
        "Microsoft.AspNetCore.Server.Kestrel",
        "System.Net.Http",
        "Romulus.Web");
}
