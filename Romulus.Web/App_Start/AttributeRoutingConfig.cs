
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AttributeRouting.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Romulus.Web.AttributeRoutingConfig), "Start")]

namespace Romulus.Web 
{
    public static class AttributeRoutingConfig
	{
		public static void RegisterRoutes(RouteCollection routes) 
		{   
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
            routes.Ignore("bond_girl.php");
            routes.Ignore("exceptions.axd");
            routes.Ignore("MvcDiagnostics.aspx");
            routes.Ignore("mvcdiagnostics.aspx");
			routes.MapAttributeRoutes();
		}

        public static void Start() 
		{
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
