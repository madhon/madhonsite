using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Romulus.Web.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace Romulus.Web
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            var container = new Container();
            container.Register<IContactService, ContactService>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcAttributeFilterProvider();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }
}