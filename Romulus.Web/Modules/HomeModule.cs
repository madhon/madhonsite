namespace Romulus.Web.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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