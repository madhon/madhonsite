namespace Romulus.Web
{
  using System;
  using JetBrains.Annotations;
  using LightInject;
  using Romulus.Web.Services;

  [UsedImplicitly]
  public class CompositionRoot : ICompositionRoot
  {
    public void Compose(IServiceRegistry serviceRegistry)
    {
      serviceRegistry.Register<ITransport, GmailTransport>();
      serviceRegistry.Register<IContactService, ContactService>();
    }
  }
}
