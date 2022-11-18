using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

AppVersionInfo.InitialiseBuildInfoGivenPath(Directory.GetCurrentDirectory());

builder.WebHost.ConfigureKestrel(o => o.AddServerHeader = false);
builder.Host.UseSystemd();

builder.Services.AddSingleton<ITransport, NullTransport>();

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

builder.Services.AddMediator();

builder.Services.AddControllersWithViews(opts => opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
	.AddFeatureFolders();

builder.Services.AddFluentValidationAutoValidation(config =>
{
	config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddFluentValidationClientsideAdapters();


var app = builder.Build();

var forwardedHeaderOptions = new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedHeaderOptions.KnownNetworks.Clear();
forwardedHeaderOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeaderOptions);

app.IfDevelopment(app.Environment, a =>
{
	a.UseDeveloperExceptionPage();
});

app.UseStaticFilesWithCacheControl(app.Environment);

app.UseRouting();

app.UseResponseCaching();
//app.UseResponseCompression();

//app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder().AddDefaultSecurePolicy());
app.SetupSecurityHeaders();

app.MapHealthChecks("/healthz");
app.MapDefaultControllerRoute();

app.Run();
