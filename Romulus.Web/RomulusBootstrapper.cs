namespace Romulus.Web
{
    using LightInject;
    using LightInject.Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Conventions;
    
    public class RomulusBootstrapper : LightInjectNancyBootstrapper
    {
        protected override byte[] FavIcon
        {
            get
            {
                return null;
            }
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            MobileViewLocationConventions.Enable(nancyConventions);
        }

        protected override void ApplicationStartup(IServiceContainer container, IPipelines pipelines)
        {
            RomulsStatusCodeHandler.AddCode(404);
            //CustomStatusCode.AddCode(ConfigurationManager.AppSettings["HttpErrorCodes"].Split(',').Select(x => int.Parse(x)));

            base.ApplicationStartup(container, pipelines);
            Nancy.Security.Csrf.Enable(pipelines);
        }
    }
}