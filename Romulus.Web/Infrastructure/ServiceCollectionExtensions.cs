namespace Romulus.Web.Infrastruture
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntiforgerySecurely(this IServiceCollection services)
        {
            return services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "f";
                    options.FormFieldName = "f";
                    options.HeaderName = "X-XSRF-TOKEN";

                });
        }

        public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
        {
            if (services == null)
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
    }
}
