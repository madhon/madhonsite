using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Nancy.Bootstrappers.Windsor;
using Nancy.Conventions;
using Romulus.Web.Infrastructure;

namespace Romulus.Web
{
    public class RomulusBootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            existingContainer.Install(new RomulusInstaller());
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            MobileViewLocationConventions.Enable(nancyConventions);
        }
    }
}