using System;
using System.Collections.Generic;
using System.Linq;
using LightInject;
using LightInject.Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;

namespace Romulus.Web
{
    public class RomulusBootstrapper : LightInjectNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            MobileViewLocationConventions.Enable(nancyConventions);
        }

        protected override void ApplicationStartup(IServiceContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Nancy.Security.Csrf.Enable(pipelines);
        }
    }
}