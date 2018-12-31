namespace Romulus.Web.Infrastructure
{
  public class SecurityHeadersBuilder
  {
    private readonly SecurityHeadersPolicy _policy = new SecurityHeadersPolicy();

    public SecurityHeadersBuilder AddDefaultSecurePolicy()
    {
      AddFrameOptionsDeny();
      AddXssProtectionBlock();
      AddContentTypeOptionsNoSniff();
      //AddStrictTransportSecurityMaxAge();
      RemoveServerHeader();
      return this;
    }

    public SecurityHeadersBuilder AddFrameOptionsDeny()
    {
      _policy.SetHeaders[FrameOptionsConstants.Header] = FrameOptionsConstants.Deny;
      return this;
    }

    public SecurityHeadersBuilder AddFrameOptionsSameOrigin()
    {
      _policy.SetHeaders[FrameOptionsConstants.Header] = FrameOptionsConstants.SameOrigin;
      return this;
    }

    public SecurityHeadersBuilder AddFrameOptionsSameOrigin(string uri)
    {
      _policy.SetHeaders[FrameOptionsConstants.Header] = string.Format(FrameOptionsConstants.AllowFromUri, uri);
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
