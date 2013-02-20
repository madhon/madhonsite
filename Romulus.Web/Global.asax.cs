using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation.Mvc;
using Microsoft.Web.Mvc;
using Romulus.Web.Infrastructure;
using StackExchange.Profiling;

namespace Romulus.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            StartIoC();

            InitProfilerSettings();
            ConfigureProfilingViewEngine();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationModelValidatorProvider.Configure();
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler profiler = null;

            var ip = Request.GetClientIpAddress();
            if (Request.IsLocal | ip == "93.96.173.32" | ip == "62.49.29.223" | ip == "89.243.253.162")
            {
                profiler = MiniProfiler.Start();
            }

            using (profiler.Step("Application_BeginRequest"))
            {
                // you can start profiling your code immediately
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        private void StartIoC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RomulusSiteModule>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void ConfigureProfilingViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ProfilingViewEngine(new FixedRazorViewEngine()));
        }

        private void InitProfilerSettings()
        {
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;
            MiniProfiler.Settings.PopupMaxTracesToShow = 10;
            MiniProfiler.Settings.RouteBasePath = "~/profiler";
            MiniProfiler.Settings.StackMaxLength = 256;

            MiniProfiler.Settings.Results_Authorize = request => true;
            MiniProfiler.Settings.Results_List_Authorize = request => true;
        }
    }
}