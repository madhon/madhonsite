namespace Romulus.Web;

internal static class ApplicationBuilderExtensions
{
    public static WebApplication UseStaticFilesWithCacheControl(this WebApplication app)
    {
        var cachePeriod = app.Environment.IsDevelopment() ? "600" : "604800";

        app.UseStaticFiles(
            new StaticFileOptions
            {
                OnPrepareResponse =
                    _ => _.Context.Response.Headers[HeaderNames.CacheControl] =
                        $"public, max-age={cachePeriod}", // A week in seconds
            });

        return app;
    }

    public static WebApplication SetupSecurityHeaders(this WebApplication app)
    {
        string preloadDirective = Debugger.IsAttached ? string.Empty : "; preload";

        app.UseSecurityHeaders(SecurityHeadersDefinitions.GetHeaderPolicyCollection(app.Environment.IsDevelopment()));
        return app;
    }
}
