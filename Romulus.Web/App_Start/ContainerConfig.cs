using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Romulus.Web.Infrastructure;

namespace Romulus.Web
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RomulusSiteModule>();

            var container = builder.Build();
            container.ActivateGlimpse();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}