using System.Reflection;
using AspNetCore.ReCaptcha;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Romulus.Web;
using Romulus.Web.Services;

var builder = WebApplication.CreateBuilder(args);

AppVersionInfo.InitialiseBuildInfoGivenPath(Directory.GetCurrentDirectory());

builder.WebHost.ConfigureKestrel(o => o.AddServerHeader = false);
builder.Host.UseSystemd();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterType<NullTransport>().As<ITransport>().InstancePerDependency();
});

// ConfigureServices migrated elements
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
	options.KnownNetworks.Clear();
	options.KnownProxies.Clear();
	options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddServerTiming();

builder.Services.AddCustomLogging(builder.Configuration, builder.Environment);

builder.Services.AddAntiForgerySecurely(builder.Environment);

builder.Services.AddRouting(options =>
{
	options.AppendTrailingSlash = true;
	options.LowercaseUrls = true;
});

builder.Services.AddHealthChecks();

builder.Services.AddResponseCaching();
//services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionMimeTypes.Defaults);

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

builder.Services.AddControllersWithViews(opts => opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
	.AddFeatureFolders()
	.AddFluentValidation(cfg =>
	{
		cfg.RegisterValidatorsFromAssemblyContaining<Program>();
	});

builder.Services.AddReCaptcha(builder.Configuration.GetSection("ReCaptcha"));

var app = builder.Build();

// ConfigureApp migrated elements

app.UseForwardedHeaders();

app.IfDevelopment(app.Environment, a =>
{
	a.UseDeveloperExceptionPage();
});

app.UseStaticFilesWithCacheControl(app.Environment);

app.UseRouting();

app.UseResponseCaching();
//app.UseResponseCompression();

app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());

app.UseEndpoints(endpoints =>
{
	endpoints.MapHealthChecks("/healthz");
	endpoints.MapDefaultControllerRoute();
});

app.Run();
