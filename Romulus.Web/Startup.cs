namespace Romulus.Web
{
    using System.Reflection;
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using JetBrains.Annotations;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiForgerySecurely();
            services.AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddResponseCaching();
            services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionMimeTypes.Defaults);

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddMvc()
                .AddFeatureFolders()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.AddTransient<ITransport, GmailTransport>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseStaticFilesWithCacheControl();

            app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy().AddFeaturePolicy().AddReferrerPolicy().AddContentSecurityPolicy());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
