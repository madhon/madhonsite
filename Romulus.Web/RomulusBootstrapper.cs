namespace Romulus.Web
{
  using DryIoc;
  using JetBrains.Annotations;
  using Nancy.Bootstrapper;
  using Nancy.Conventions;
  using Nancy.Security;
  using Romulus.Web.Services;

  [UsedImplicitly]
  public class RomulusBootstrapper : DryIocNancyBootstrapper
  {
    protected override byte[] FavIcon
    {
      get { return null; }
    }

    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      base.ConfigureConventions(nancyConventions);
      MobileViewLocationConventions.Enable(nancyConventions);
    }

    protected override void ConfigureApplicationContainer(IContainer existingContainer)
    {
      base.ConfigureApplicationContainer(existingContainer);

      existingContainer.Register<ITransport, GmailTransport>();
      existingContainer.Register<IContactService, ContactService>();
    }

    protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
    {
      RomulsStatusCodeHandler.AddCode(404);
      //CustomStatusCode.AddCode(ConfigurationManager.AppSettings["HttpErrorCodes"].Split(',').Select(x => int.Parse(x)));
      Csrf.Enable(pipelines);
      base.ApplicationStartup(container, pipelines);
    }
  }
}
