using System;
using System.Collections.Generic;
using System.Linq;
using Romulus.Web.Services;
using LightInject;


namespace Romulus.Web
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            var container = new ServiceContainer();
            container.RegisterControllers();
            container.Register<IContactService, ContactService>();
            container.EnableMvc();
        }

    }
}