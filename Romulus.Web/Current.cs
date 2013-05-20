using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Romulus.Web.Infrastructure;
using StackExchange.Profiling;

namespace Romulus.Web
{
    public static class Current
    {
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public static HttpRequest Request
        {
            get { return Context.Request; }
        }

        public static Controller Controller
        {
            get { return Context.Items["Controller"] as Controller; }
            set { Context.Items["Controller"] = value; }
        }

        public static MiniProfiler Profiler
        {
            get { return MiniProfiler.Current; }
        }

        public static bool IsAdmin
        {
            get
            {
                var ip = Request.GetClientIpAddress();
                if (Request.IsLocal | ip == "93.96.173.32" | ip == "62.49.29.223" | ip == "89.243.253.162")
                {
                    return true;
                }

                return false;
            }
        }
    }
}