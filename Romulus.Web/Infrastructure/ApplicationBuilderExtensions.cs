namespace Romulus.Web
{
	using System;
	using System.Diagnostics;
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

        public static IApplicationBuilder IfDevelopment(this IApplicationBuilder builder, IHostEnvironment env,
	        Action<IApplicationBuilder> operation)
        {
	        if (env.IsDevelopment() && operation is not null)
	        {
		        operation(builder);
	        }

	        return builder;
        }

        public static IApplicationBuilder SetupSecurityHeaders(this IApplicationBuilder app)
        {
	        app.UseSecurityHeaders(policies =>
		        policies
			        .AddDefaultSecurityHeaders()
			        .RemoveServerHeader()
			        .AddReferrerPolicyNoReferrerWhenDowngrade()
			        .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: 31536000)
			        //.AddStrictTransportSecurityMaxAgeIncludeSubDomainsAndPreload(maxAgeInSeconds: 31536000)
			        
			        .AddContentSecurityPolicy(p =>
			        {
				        p.AddUpgradeInsecureRequests();
				        p.AddBlockAllMixedContent();
				        p.AddDefaultSrc().None();
				        p.AddObjectSrc().None();
				        p.AddMediaSrc().None();
				        p.AddFrameAncestors().None();
				        p.AddFrameSrc().None();
				        p.AddFormAction().Self();
				        p.AddImgSrc().Self();
						p.AddManifestSrc().Self();

						if (Debugger.IsAttached)
						{
							p.AddConnectSrc()
								.Self()
								.From("http://localhost:*")
								.From("https://localhost:*")
								.From("ws://localhost:*");

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

						}
						else
						{
							p.AddConnectSrc().None();
							p.AddScriptSrc()
								.Self()
								.From("https://ajax.googleapis.com")
								.From("https://cdn.jsdelivr.net")
								.From("https://cdnjs.cloudflare.com")
								.From("https://unpkg.com")
								;

							p.AddStyleSrc()
								.Self()
								.From("https://cdn.jsdelivr.net")
								.From("https://fonts.googleapis.com")
								.From("https://fonts.gstatic.com")
								;
						}


						p.AddFontSrc()
					        .From("https://fonts.gstatic.com");

			        })
			        .AddPermissionsPolicy(p =>
			        {
				        p.AddAccelerometer().None();
				        p.AddAutoplay().None();
				        p.AddCamera().None();
				        p.AddEncryptedMedia().None();
				        p.AddFullscreen().None();
				        p.AddGeolocation().None();
				        p.AddGyroscope().None();
				        p.AddMagnetometer().None();
				        p.AddMicrophone().None();
				        p.AddMidi().None();
				        p.AddPayment().None();
				        p.AddPictureInPicture().None();
				        p.AddSpeaker().None();
				        p.AddUsb().None();
			        })
			        .AddCustomHeader("Expect-CT", "max-age=0")

			        
	        );
	        return app;
        }
    }
}
