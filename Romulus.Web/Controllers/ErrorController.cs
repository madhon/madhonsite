using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Romulus.Web.Infrastructure;

namespace Romulus.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("404")]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return new ContentResult {Content = "Not Found (this is a 404 page)"};
        }

    }
}
