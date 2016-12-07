namespace Romulus.Web
{
    using Autofac;
    using JetBrains.Annotations;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Conventions;
    using Nancy.Gzip;
    using Nancy.Security;
    using Romulus.Web.Helpers;

    [UsedImplicitly]
    public class RomulusBootstrapper : AutofacNancyBootstrapper
    {
        protected override byte[] FavIcon => null;

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.EnableMobileViewLocationConventions();
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            RomulsStatusCodeHandler.AddCode(404);
            //CustomStatusCode.AddCode(ConfigurationManager.AppSettings["HttpErrorCodes"].Split(',').Select(x => int.Parse(x)));
            Csrf.Enable(pipelines);

            pipelines.EnableGzipCompression();

            base.ApplicationStartup(container, pipelines);
        }
    }
}