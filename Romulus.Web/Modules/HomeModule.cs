namespace Romulus.Web.Modules
{
  using System;
  using JetBrains.Annotations;

  [UsedImplicitly]
  public class HomeModule : BaseModule
    {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
          base.Page.Title = "Home";
          return View["Views/Home/Index", base.Model];
      };
    }
  }
}
