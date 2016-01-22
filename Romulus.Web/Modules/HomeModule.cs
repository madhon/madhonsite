namespace Romulus.Web.Modules
{
  using System;
  using JetBrains.Annotations;
  using Nancy;

  [UsedImplicitly]
  public class HomeModule : BaseModule
    {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        ViewBag.title = "Home";
        return View["Views/Home/Index"];
      };
    }
  }
}
