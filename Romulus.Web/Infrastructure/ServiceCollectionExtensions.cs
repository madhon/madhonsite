namespace Romulus.Web;

using Romulus.Web.Features.Contact;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Send.Command>, Send.CommandValidator>();
        return services;
    }

    public static IServiceCollection AddMailTransport(this IServiceCollection services)
    {
        services.AddScoped<ITransport, NullTransport>();
        return services;
    }

    public static IServiceCollection AddAntiForgerySecurely(this IServiceCollection services)
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
        ArgumentNullException.ThrowIfNull(services);

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

    public static IServiceCollection AddServerTiming(this IServiceCollection services) =>
        services.AddSingleton<ServerTimingMiddleware>();
}
