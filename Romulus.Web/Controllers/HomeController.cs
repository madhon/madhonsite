using System;
using System.Web.Mvc;
using StackExchange.Exceptional;

namespace Romulus.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Errors()
        {
            var context = System.Web.HttpContext.Current;
            var factory = new HandlerFactory();

            var page = factory.GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }
    }
}