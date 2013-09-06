using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [GET("Error/General")]
        public ActionResult General()
        {
            Response.StatusCode = 500;
            return View("General");
        }

        [HttpGet]
        [GET("Error/PageNotFound")]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View("PageNotFound");
        }

    }
}
