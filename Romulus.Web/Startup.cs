namespace Romulus.Web
{
    using System.Reflection;
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using JetBrains.Annotations;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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

            services.AddControllersWithViews()
                .AddFeatureFolders()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.AddTransient<ITransport, NullTransport>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
              ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


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
            app.UseStaticFilesWithCacheControl(env);

            app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy().AddFeaturePolicy().AddReferrerPolicy().AddContentSecurityPolicy());

            app.UseRouting();

            app.UseEndpoints(endpoints => 
            {
              endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
