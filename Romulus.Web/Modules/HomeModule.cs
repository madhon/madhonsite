namespace Romulus.Web.Modules
{
  using JetBrains.Annotations;

  [UsedImplicitly]
  public class HomeModule : BaseModule
    {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
          Page.Title = "Home";
          return View["Views/Home/Index", Model];
      };
    }
  }
}
