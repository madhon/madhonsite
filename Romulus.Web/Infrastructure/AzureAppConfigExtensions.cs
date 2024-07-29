namespace Romulus.Web.Infrastructure;

using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

public static class AzureAppConfigExtensions
{
    public static IServiceCollection AddAzureAppConfig(this IServiceCollection services,
        IConfigurationBuilder configurationBuilder,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (string.IsNullOrEmpty(configuration.GetConnectionString("AppConfig")))
        {
            return services;
        }

        var azCredOpts = new DefaultAzureCredentialOptions
        {
            ExcludeAzureCliCredential = false,
            ExcludeAzureDeveloperCliCredential = false,
            ExcludeAzurePowerShellCredential = false,
            ExcludeEnvironmentCredential = false,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeManagedIdentityCredential = false,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = false,
            ExcludeVisualStudioCredential = false,
            ExcludeWorkloadIdentityCredential = true,
        };

        //var azEventSourceListener = AzureEventSourceListener.CreateConsoleLogger(System.Diagnostics.Tracing.EventLevel.Verbose);

        configurationBuilder.AddAzureAppConfiguration(opts =>
        {
            var aazOpts = configuration.GetConnectionString("AppConfig");

            opts.Connect(
                    new Uri(aazOpts!),
                    new DefaultAzureCredential(azCredOpts))
                .Select(KeyFilter.Any, LabelFilter.Null);
        });

        // evil hack to re-add user secrets as azure app config overrides it
        if (environment.IsDevelopment())
        {
            var appAssembly = Assembly.Load(new AssemblyName(environment.ApplicationName));
            configurationBuilder.AddUserSecrets(appAssembly, optional: true, reloadOnChange: true);
        }

        return services;
    }
}
