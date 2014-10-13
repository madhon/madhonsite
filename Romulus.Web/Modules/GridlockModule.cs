using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace Romulus.Web.Modules
{
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