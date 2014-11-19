using System;
using System.Collections.Generic;
using System.Linq;
using LightInject;
using LightInject.Nancy;
using Nancy.Conventions;

namespace Romulus.Web
{
    public class RomulusBootstrapper : LightInjectNancyBootstrapper
    {
        protected override IServiceContainer GetServiceContainer()
        {
            return base.GetServiceContainer();
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            MobileViewLocationConventions.Enable(nancyConventions);
        }
    }
}