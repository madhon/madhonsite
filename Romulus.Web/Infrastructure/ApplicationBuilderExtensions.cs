namespace Romulus.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Net.Http.Headers;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            return application.UseStaticFiles(
                new StaticFileOptions
                {
                    OnPrepareResponse =
                        _ => _.Context.Response.Headers[HeaderNames.CacheControl] =
                            "public,max-age=604800" // A week in seconds
                });
        }
    }
}
