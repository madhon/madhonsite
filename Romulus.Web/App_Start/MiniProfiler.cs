using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Romulus.Web.App_Start;
using Romulus.Web.Infrastructure;
using StackExchange.Profiling;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MiniProfilerPackage), "PreStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(MiniProfilerPackage), "PostStart")]

namespace Romulus.Web.App_Start
{
    public static class MiniProfilerPackage
    {
        public static void PreStart()
        {
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Left;
            MiniProfiler.Settings.PopupMaxTracesToShow = 10;
            MiniProfiler.Settings.RouteBasePath = "~/profiler";
            MiniProfiler.Settings.StackMaxLength = 256;
            MiniProfiler.Settings.Results_Authorize = request => Current.IsAdmin;
            MiniProfiler.Settings.Results_List_Authorize = request => Current.IsAdmin;

            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();
            ignored.Add("WebResource.axd");
            ignored.Add("ScriptResource.axd");
            ignored.Add("Glimpse.axd");
            ignored.Add("/content/");
            ignored.Add("/img/");
            ignored.Add("/scripts/");
            ignored.Add(".js");

            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();

            DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
        }

        public static void PostStart()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ProfilingViewEngine(new RazorViewEngine()));
            //ViewEngines.Engines.Add(new FixedRazorViewEngine());
        }
    }

    public class MiniProfilerStartupModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => MiniProfiler.Start();
            context.EndRequest += (sender, e) => MiniProfiler.Stop();
        }

        public void Dispose()
        {
        }
    }
}