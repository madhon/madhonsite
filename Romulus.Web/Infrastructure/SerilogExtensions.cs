namespace Romulus.Web.Infrastructure;

using System.Globalization;
using Serilog.Settings.Configuration;

internal static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = new SerilogOptions();
        builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

        builder.Services.AddSerilog(loggerConfiguration =>
        {
            var options = new ConfigurationReaderOptions { SectionName = sectionName };
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration, options);

            loggerConfiguration
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext();

            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
            loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

            if (serilogOptions.UseConsole)
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                    writeTo.Console(outputTemplate: serilogOptions.LogTemplate, formatProvider: CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(serilogOptions.FilePath))
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                {
                    writeTo.File(serilogOptions.FilePath,
                        outputTemplate: serilogOptions.LogTemplate,
                        formatProvider: CultureInfo.InvariantCulture,
                        shared: true,
                        rollingInterval: RollingInterval.Day);
                });
            }
        });

        return builder;
    }

    internal sealed class SerilogOptions
    {
        public bool UseConsole { get; set; } = true;
        public string? FilePath
        {
            get; set;
        }
        public string? SeqUrl
        {
            get; set;
        }

        public string LogTemplate
        {
            get; set;
        } =
            "[{Timestamp:yyyy-MM-dd HH:mm} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    }
}
