using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
//using Autofac;
//using Autofac.Integration.Mvc;
//using Romulus.Web.Infrastructure;
using Romulus.Web.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Romulus.Web
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            // Autofac support code
            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterModule<RomulusSiteModule>();
            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // SimpleInjector support code
            var container = new Container();
            container.Register<IContactService, ContactService>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }
}