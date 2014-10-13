using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace Romulus.Web.Modules
{
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