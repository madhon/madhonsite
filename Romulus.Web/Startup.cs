namespace Romulus.Web
{
	using System.Reflection;
	using FluentValidation.AspNetCore;
	using Infrastructure;
	using JetBrains.Annotations;
	using MediatR;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.HttpOverrides;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Services;

	public class Startup
	{
		public IConfiguration Configuration
		{
			get;
		}
		public IHostEnvironment Environment
		{
			get;
		}

		public Startup(IConfiguration configuration, IHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}
		
		[UsedImplicitly]
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.KnownNetworks.Clear();
				options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});
			
			services.AddCustomLogging(Configuration, Environment);

			services.AddAntiForgerySecurely(Environment);

			services.AddRouting(options =>
			{
				options.AppendTrailingSlash = true;
				options.LowercaseUrls = true;
			});
			
			services.AddHealthChecks();

			services.AddResponseCaching();
			//services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionMimeTypes.Defaults);

			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

			services.AddControllersWithViews(opts => opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
				.AddFeatureFolders()
				.AddFluentValidation(cfg =>
				{
					cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
				});

			services.AddTransient<ITransport, NullTransport>();
		}

		[UsedImplicitly]
		public void Configure(IApplicationBuilder app)
		{
			app.UseForwardedHeaders();

			app.IfDevelopment(Environment, a =>
			{
				a.UseDeveloperExceptionPage();
			});
			
			app.UseStaticFilesWithCacheControl(Environment);

			app.UseRouting();

			app.UseResponseCaching();
			//app.UseResponseCompression();

			app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/healthz");
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
