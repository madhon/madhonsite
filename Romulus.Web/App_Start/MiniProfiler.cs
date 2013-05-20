using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Microsoft.Web.Mvc;
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
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;
            MiniProfiler.Settings.PopupMaxTracesToShow = 10;
            MiniProfiler.Settings.RouteBasePath = "~/profiler";
            MiniProfiler.Settings.StackMaxLength = 256;
            DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));

            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            MiniProfiler.Settings.Results_Authorize = request => Current.IsAdmin;
            MiniProfiler.Settings.Results_List_Authorize = request => Current.IsAdmin;
           
        }

        public static void PostStart()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ProfilingViewEngine(new FixedRazorViewEngine()));
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