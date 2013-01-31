using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Romulus.Web.Services;

namespace Romulus.Web.Infrastructure
{
    public class RomulusSiteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactService>().As<IContactService>();
        }
    }
}