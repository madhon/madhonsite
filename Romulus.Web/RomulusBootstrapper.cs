namespace Romulus.Web
{
    using System;
    using System.Collections.Generic;
    using DryIoc;
    using JetBrains.Annotations;
    using Nancy.Bootstrapper;
    using Nancy.Conventions;
    using Nancy.Gzip;
    using Nancy.Security;
    using Romulus.Web.Services;
    using Romulus.Web.Helpers;

    [UsedImplicitly]
    public class RomulusBootstrapper : DryIocNancyBootstrapper
    {
        protected override byte[] FavIcon => null;

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.EnableMobileViewLocationConventions();
        }

        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Register<ITransport, GmailTransport>();
            existingContainer.Register<IContactService, ContactService>();
        }

        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            RomulsStatusCodeHandler.AddCode(404);
            //CustomStatusCode.AddCode(ConfigurationManager.AppSettings["HttpErrorCodes"].Split(',').Select(x => int.Parse(x)));
            Csrf.Enable(pipelines);

            pipelines.EnableGzipCompression();

            base.ApplicationStartup(container, pipelines);
        }
    }
}