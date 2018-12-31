namespace Romulus.Web
{
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using Infrastruture;
    using JetBrains.Annotations;
    using Lamar;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Startup
    {
        [UsedImplicitly]
        public void ConfigureContainer(ServiceRegistry services)
        {
            services.AddAntiforgerySecurely();
            services.AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddResponseCaching();
            services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionMimeTypes.Defaults);

            services.AddMediatR();
            services.AddMvc()
                .AddFeatureFolders()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.For<ITransport>().Use<GmailTransport>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseStaticFilesWithCacheControl();

          app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
