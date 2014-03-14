using System;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using StackExchange.Exceptional;

namespace Romulus.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [GET("")]
        [GET("", IsAbsoluteUrl = true, ActionPrecedence = 1)]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [GET("Errors/{resource?}/{subResource?}")]
        public ActionResult Errors()
        {
            var context = System.Web.HttpContext.Current;
            var page = new HandlerFactory().GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);
            return null;
        }
    }
}