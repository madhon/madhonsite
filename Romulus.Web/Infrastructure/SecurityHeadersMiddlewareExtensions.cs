namespace Romulus.Web.Infrastructure
{
  using Microsoft.AspNetCore.Builder;

  public static class SecurityHeadersMiddlewareExtensions
  {
    public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
    {
      SecurityHeadersPolicy policy = builder.Build();
      return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
    }

  }
}
