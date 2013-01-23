using System;
using System.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class GridlockController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
