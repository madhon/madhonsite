using System;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class BondGirlController : Controller
    {
        [HttpGet]
        [GET("BondGirl")]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
