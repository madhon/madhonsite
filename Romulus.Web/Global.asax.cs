using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;

namespace Romulus.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
			MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerConfig.RegisterContainer();

            FluentValidationModelValidatorProvider.Configure();
        }

        protected void Application_BeginRequest()
        {
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_EndRequest()
        {
        }
    }
}