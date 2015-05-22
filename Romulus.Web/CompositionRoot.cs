namespace Romulus.Web
{
    using LightInject;
    using Romulus.Web.Services;

    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ITransport, GmailTransport>();
            serviceRegistry.Register<IContactService, ContactService>();
        }
    }
}