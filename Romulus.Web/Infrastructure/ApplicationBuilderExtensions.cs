namespace Romulus.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Net.Http.Headers;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application, IHostEnvironment env)
        {
          var cachePeriod = env.IsDevelopment() ? "600" : "604800";

          return application.UseStaticFiles(
                new StaticFileOptions
                {
                    OnPrepareResponse =
                        _ => _.Context.Response.Headers[HeaderNames.CacheControl] =
                          $"public, max-age={cachePeriod}" // A week in seconds
                });
        }
    }
}
