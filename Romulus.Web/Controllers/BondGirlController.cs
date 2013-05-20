using System;
using System.Web.Mvc;
using Romulus.Web.Infrastructure;

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
