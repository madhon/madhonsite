using System;
using System.Web.Mvc;

namespace Romulus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Errors()
        {
            var context = System.Web.HttpContext.Current;
            var factory = new StackExchange.Exceptional.HandlerFactory();

            var page = factory.GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }
    }
}
