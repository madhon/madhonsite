using System;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class BondGirlController : Controller
    {
        [HttpGet]
        [GET("BondGirl")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
