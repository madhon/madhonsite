using System;
using System.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class BondGirlController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
