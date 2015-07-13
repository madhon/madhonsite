namespace Romulus.Web.Modules
{
  using System;
  using JetBrains.Annotations;
  using Nancy;

  [UsedImplicitly]
  public class GridlockModule : NancyModule
  {
    public GridlockModule()
    {
      Get["/gridlock"] = _ => GetGridlockPage(_);
    }

    private dynamic GetGridlockPage(dynamic p)
    {
      ViewBag.title = "Gridlock";
      return View["Views/Gridlock/Index"];
    }
  }
}
