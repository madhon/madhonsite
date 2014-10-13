using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Romulus.Web.Services;

namespace Romulus.Web.Infrastructure
{
    public class RomulusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IContactService>().ImplementedBy<ContactService>());
        }
    }
}