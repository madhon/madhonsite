using System;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class GridlockController : Controller
    {
        [HttpGet]
        [GET("Gridlock")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
