namespace Romulus.Web.Modules
{
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class GridlockModule : BaseModule
    {
        public GridlockModule()
        {
            Get["/gridlock"] = _ => GetGridlockPage();
        }

        private dynamic GetGridlockPage()
        {
            Page.Title = "Gridlock";
            return View["Views/Gridlock/Index", Model];
        }
    }
}