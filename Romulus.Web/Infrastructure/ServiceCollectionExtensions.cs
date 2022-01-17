namespace Romulus.Web
{
  using System;
  using JetBrains.Annotations;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using Romulus.Web.Infrastructure;

  public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntiForgerySecurely(this IServiceCollection services, IHostEnvironment env)
        {
            return services.AddAntiforgery(
                options =>
                {
	                options.SuppressXFrameOptionsHeader = true;
                    options.Cookie.Name = "f";
                    options.FormFieldName = "f";
                    //options.Cookie.SecurePolicy = env.IsDevelopment()
	                   // ? CookieSecurePolicy.SameAsRequest
	                   // : CookieSecurePolicy.Always;
	                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Cookie.HttpOnly = true;
					options.HeaderName = "X-XSRF-TOKEN";
                });
        }

        public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var expander = new FeatureViewLocationExpander();

            services.AddMvcOptions(o => o.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(o =>
                {
                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name
                    // {3} - Feature Name
                    // Replace normal view location entirely
                    o.ViewLocationFormats.Clear();
                    o.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                    o.ViewLocationExpanders.Add(expander);
                });

            return services;
        }

        public static void AddCustomLogging([NotNull] this IServiceCollection services, [NotNull] IConfiguration configuration, IHostEnvironment environment)
        {
	        services.AddLogging(config =>
	        {
		        config.ClearProviders();
		        config.AddConfiguration(configuration.GetSection("Logging"));
		        config.AddDebug();
		        config.AddEventSourceLogger();

		        if (environment.IsDevelopment())
		        {
			        config.AddConsole();
		        }
	        });
        }

        public static IServiceCollection AddServerTiming(this IServiceCollection services) =>
	        services.AddSingleton<ServerTimingMiddleware>();

	}
}
