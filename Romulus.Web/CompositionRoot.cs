namespace Romulus.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LightInject;
    using Romulus.Web.Services;

    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IContactService, ContactService>();
        }
    }
}