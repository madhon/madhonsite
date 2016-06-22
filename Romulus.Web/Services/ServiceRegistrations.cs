namespace Romulus.Web.Services
{
    using JetBrains.Annotations;
    using Nancy.Bootstrapper;

    [UsedImplicitly]
    public class ServiceRegistrations : Registrations
    {
        public ServiceRegistrations()
        {
            Register<ITransport>(typeof(GmailTransport));
            Register<IContactService>(typeof(ContactService));
        }
    }
}