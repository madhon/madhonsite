using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult General()
        {
            Response.StatusCode = 500;
            return View("General");
        }

        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View("PageNotFound");
        }

    }
}
