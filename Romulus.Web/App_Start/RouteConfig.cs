using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Romulus.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});

            routes.Ignore("bond_girl.php");
            routes.Ignore("exceptions.axd");

            //RouteAttribute.MapDecoratedRoutes(routes);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Errors",
                url: "{controller}/{action}/{resource}/{subResource}",
                defaults: new
                    {
                        controller = "Home",
                        action = "Errors",
                        resource = UrlParameter.Optional,
                        subResource = UrlParameter.Optional
                    }
                );

            routes.MapRoute(
                name: "Root",
                url: "",
                defaults: new {controller = "Home", action = "Index", id = ""}
                );

            routes.MapRoute(
                name: "",
                url: "{*url}",
                defaults: new {controller = "Error", action = "PageNotFound"});
        }
    }
}