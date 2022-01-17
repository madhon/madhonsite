namespace Romulus.Web
{
  using Microsoft.AspNetCore.Builder;
  using Romulus.Web.Infrastructure;

  public static class SecurityHeadersMiddlewareExtensions
  {
    public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
    {
      SecurityHeadersPolicy policy = builder.Build();
      return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
    }

  }
}
