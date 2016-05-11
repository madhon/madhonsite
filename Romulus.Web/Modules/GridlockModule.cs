namespace Romulus.Web.Modules
{
    using System;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class GridlockModule : BaseModule
    {
        public GridlockModule()
        {
            Get["/gridlock"] = _ => GetGridlockPage(_);
        }

        private dynamic GetGridlockPage(dynamic p)
        {
            Page.Title = "Gridlock";
            return View["Views/Gridlock/Index", base.Model];
        }
    }
}