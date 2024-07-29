namespace Romulus.Web.Infrastructure;

using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class OpenTelemetryExtensions
{
    public static IHostApplicationBuilder AddOpenTelemetry(this IHostApplicationBuilder builder)
    {
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

        var honeycombOptions = builder.Configuration.GetHoneycombOptions();
        if (honeycombOptions is not null && !string.IsNullOrEmpty(honeycombOptions.ApiKey))
        {
            builder.Services.AddOpenTelemetry()
                .WithTracing(tracing => tracing.AddHoneycomb(honeycombOptions))
                .WithMetrics(metrics => metrics.AddHoneycomb(honeycombOptions));
        }

        return builder;
    }

    private static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
    meterProviderBuilder.AddMeter(
        "Microsoft.AspNetCore.Hosting",
        "Microsoft.AspNetCore.Server.Kestrel",
        "System.Net.Http");
}
