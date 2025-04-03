namespace Romulus.Web.Infrastructure;

public static class SecurityHeadersDefinitions
{
    public static HeaderPolicyCollection GetHeaderPolicyCollection(bool cspReportOnly)
    {
        string preloadDirective = Debugger.IsAttached ? string.Empty : "; preload";

        var policy = new HeaderPolicyCollection()
            .AddFrameOptionsDeny()
            .AddXssProtectionBlock()
            .AddContentTypeOptionsNoSniff()
            .AddReferrerPolicyStrictOriginWhenCrossOrigin()
            .RemoveServerHeader()
            .AddPermissionsPolicy(p =>
            {
                p.AddAccelerometer().None();
                p.AddAutoplay().None();
                p.AddCamera().None();
                p.AddEncryptedMedia().None();
                p.AddFullscreen().All();
                p.AddGyroscope().None();
                p.AddMagnetometer().None();
                p.AddMicrophone().None();
                p.AddMidi().None();
                p.AddPayment().None();
                p.AddPictureInPicture().None();
                p.AddSyncXHR().Self();
                p.AddUsb().None();
                p.AddGeolocation().Self();
            });

        policy.AddCustomHeader("Strict-Transport-Security", $"max-age=31536000; includeSubDomains{preloadDirective}");

        AddCspHstsDefinitions(cspReportOnly, policy);

        return policy;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "MA0051:Method is too long", Justification = "<Pending>")]
    private static void AddCspHstsDefinitions(bool cspReportOnly, HeaderPolicyCollection policy)
    {
        if (!Debugger.IsAttached)
        {
            //policy when running on digital ocean
            policy.AddContentSecurityPolicy(p =>
            {
                if (!cspReportOnly)
                {
                    // only add this when not in report only mode as it is not valid in that situation
                    p.AddUpgradeInsecureRequests();
                }

                p.AddBlockAllMixedContent();
                p.AddDefaultSrc().Self();
                p.AddObjectSrc().Self();
                p.AddFormAction().Self();
                p.AddFrameAncestors().None();
                p.AddMediaSrc().Self();
                p.AddBaseUri().Self();
                p.AddFrameSrc().Self();

                p.AddConnectSrc().None();

                p.AddImgSrc()
                    .Self();

                p.AddScriptSrc()
                    .Self()
                    .From("https://ajax.googleapis.com")
                    .From("https://cdn.jsdelivr.net")
                    .From("https://cdnjs.cloudflare.com")
                    .From("https://unpkg.com");

                p.AddStyleSrc()
                    .Self()
                    .From("https://cdn.jsdelivr.net")
                    .From("https://fonts.googleapis.com")
                    .From("https://fonts.gstatic.com");

                p.AddFontSrc()
                    .From("https://fonts.gstatic.com");
            }, asReportOnly: cspReportOnly);
        }
        else
        {
            //policy when running in visual studio
            policy.AddContentSecurityPolicy(p =>
            {
                if (!cspReportOnly)
                {
                    // only add this when not in report only mode as it is not valid in that situation
                    p.AddUpgradeInsecureRequests();
                }

                p.AddBlockAllMixedContent();
                p.AddDefaultSrc().Self();
                p.AddObjectSrc().Self();
                p.AddFormAction().Self();
                p.AddFrameAncestors().None();
                p.AddMediaSrc().Self();
                p.AddBaseUri().Self();
                p.AddFrameSrc().Self();

                p.AddConnectSrc()
                    .Self()
                    .From("http://localhost:*")
                    .From("https://localhost:*")
                    .From("ws://localhost:*");

                p.AddImgSrc()
                    .Self()
                    .UnsafeInline();

                p.AddScriptSrc()
                    .Self()
                    .From("https://ajax.googleapis.com")
                    .From("https://cdn.jsdelivr.net")
                    .From("https://cdnjs.cloudflare.com")
                    .From("https://unpkg.com")
                    .UnsafeInline();

                p.AddStyleSrc()
                    .Self()
                    .From("https://cdn.jsdelivr.net")
                    .From("https://fonts.googleapis.com")
                    .From("https://fonts.gstatic.com")
                    .UnsafeInline();

                p.AddFontSrc()
                    .From("https://fonts.gstatic.com")
                    .UnsafeInline();
            }, asReportOnly: cspReportOnly);
        }
    }
}
