namespace Romulus.Web.Infrastructure
{
  public class SecurityHeadersBuilder
  {
    private readonly SecurityHeadersPolicy _policy = new SecurityHeadersPolicy();

    public SecurityHeadersBuilder AddDefaultSecurePolicy()
    {
      //AddFrameOptionsDeny();
      //AddDownloadOptionsNoOpen();
      //AddContentTypeOptionsNoSniff();
      //AddXssProtectionBlock();
      //AddStrictTransportSecurityMaxAge();
      RemoveServerHeader();
      RemoveHeader("X-Powered-By");
      return this;
    }

    public SecurityHeadersBuilder AddFrameOptionsDeny()
    {
      _policy.SetHeaders[FrameOptionsConstants.Header] = FrameOptionsConstants.Deny;
      return this;
    }

    public SecurityHeadersBuilder AddXssProtectionBlock()
    {
      _policy.SetHeaders["X-XSS-Protection"] = "1; mode=block";
      return this;
    }

    public SecurityHeadersBuilder AddContentTypeOptionsNoSniff()
    {
      _policy.SetHeaders["X-Content-Type-Options"] = "nosniff";
      return this;
    }

    public SecurityHeadersBuilder AddDownloadOptionsNoOpen()
    {
      _policy.SetHeaders["X-Download-Options"] = "noopen";
      return this;
    }

    public SecurityHeadersBuilder AddFeaturePolicy()
    {
      _policy.SetHeaders["Feature-Policy"] = @"geolocation 'none'; midi 'none'; notifications 'none';push 'none'; microphone 'none'; camera 'none'; magnetometer 'none'; gyroscope 'none'; speaker 'none'; vibrate 'none'; payment 'none'";
      return this;
    }

    public SecurityHeadersBuilder AddContentSecurityPolicy()
    {
      _policy.SetHeaders["Content-Security-Policy"] = @"script-src 'self' ajax.googleapis.com https://www.google-analytics.com cdnjs.cloudflare.com stackpath.bootstrapcdn.com; style-src 'self' stackpath.bootstrapcdn.com cdnjs.cloudflare.com use.fontawesome.com fonts.googleapis.com; img-src 'self' www.google-analytics.com https://stats.g.doubleclick.net; connect-src 'self' www.google-analytics.com https://stats.g.doubleclick.net; form-action 'self'";
      return this;
    }

    public SecurityHeadersBuilder AddReferrerPolicy()
    {
      _policy.SetHeaders["referrer"] = "no-referrer-when-downgrade";
      _policy.SetHeaders["Referrer-Policy"] = "no-referrer-when-downgrade";
      return this;
    }

    public SecurityHeadersBuilder RemoveServerHeader()
    {
      _policy.RemoveHeaders.Add("Server");
      return this;
    }

    public SecurityHeadersBuilder AddCustomHeader(string header, string value)
    {
      _policy.SetHeaders[header] = value;
      return this;
    }

    public SecurityHeadersBuilder RemoveHeader(string header)
    {
      _policy.RemoveHeaders.Add(header);
      return this;
    }

    public SecurityHeadersPolicy Build()
    {
      return _policy;
    }
  }
}
