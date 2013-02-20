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
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.Ignore("bond_girl.php");

            routes.MapRoute(
                            "Default",
                            "{controller}.aspx/{action}/{id}",
                            new { action = "Index", id = "" }
                          );

            routes.MapRoute(
                          "Root",
                          "",
                          new { controller = "Home", action = "Index", id = "" }
                        );
        }
    }
}