namespace Romulus.Web.Modules
{
    using Nancy;

    public class HomeModule : NancyModule
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