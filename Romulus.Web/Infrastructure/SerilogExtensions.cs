namespace Romulus.Web.Infrastructure;

using Serilog.Settings.Configuration;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder host, IConfiguration configuration, IWebHostEnvironment environment, string sectionName = "Serilog")
    {
        var serilogOptions = new SerilogOptions();
        configuration.GetSection(sectionName).Bind(serilogOptions);

        host.UseSerilog((context, loggerConfiguration) =>
        {
            var options = new ConfigurationReaderOptions { SectionName = sectionName };
            loggerConfiguration.ReadFrom.Configuration(context.Configuration, options);

            loggerConfiguration
                .Enrich.WithProperty("Application", environment.ApplicationName)
                .Enrich.FromLogContext();

            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
            loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

            if (serilogOptions.UseConsole)
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                {
                    writeTo.Console(outputTemplate: serilogOptions.LogTemplate);
                });
            }

            if (!string.IsNullOrEmpty(serilogOptions.FilePath))
            {
                loggerConfiguration.WriteTo.Async(writeTo =>
                {
                    writeTo.File(serilogOptions.FilePath,
                        outputTemplate: serilogOptions.LogTemplate,
                        rollingInterval: RollingInterval.Day, shared: true);
                });
            }

        });

        return host;
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
