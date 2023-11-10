var builder = WebApplication.CreateBuilder(args);

AppVersionInfo.InitialiseBuildInfoGivenPath(Directory.GetCurrentDirectory());

builder.Services.AddAzureAppConfig(builder.Configuration, builder.Configuration, builder.Environment);

builder.Host.AddSerilog(builder.Configuration, builder.Environment);
builder.Services.AddOpenTelemetry(builder.Configuration, builder.Environment);

builder.WebHost.ConfigureKestrel(o => o.AddServerHeader = false);
builder.Host.UseSystemd();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.AppendTrailingSlash = true;
    options.LowercaseUrls = true;
});

builder.Services.AddScoped<ITransport, NullTransport>();

builder.Services.AddServerTiming();


builder.Services.AddAntiForgerySecurely(builder.Environment);

builder.Services.AddRouting();

builder.Services.AddHealthChecks();

builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionMimeTypes.Defaults);

builder.Services.AddMediator(opts=> opts.ServiceLifetime = ServiceLifetime.Scoped);

builder.Services.AddControllersWithViews(opts => opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
	.AddFeatureFolders();

builder.Services.AddFluentValidationAutoValidation(config =>
{
	config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddFeatureManagement();

var app = builder.Build();

app.UseForwardedHeaders();

app.MapHealthChecks("/healthz");

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFilesWithCacheControl();

app.UseRouting();

app.UseResponseCaching();
app.UseResponseCompression();

app.SetupSecurityHeaders();

app.MapDefaultControllerRoute();

app.Run();
