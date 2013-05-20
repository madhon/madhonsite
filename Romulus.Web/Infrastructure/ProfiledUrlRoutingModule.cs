using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using StackExchange.Profiling;

namespace Romulus.Web.Infrastructure
{
    public class ProfiledUrlRoutingModule : UrlRoutingModule
    {
        public override void PostResolveRequestCache(HttpContextBase context)
        {
            if (context.Request.Path.StartsWith("/content/", StringComparison.OrdinalIgnoreCase)) return;
            if (context.Request.Path.StartsWith("/includes/", StringComparison.OrdinalIgnoreCase)) return;
            if (context.Request.Path.StartsWith("/img/", StringComparison.OrdinalIgnoreCase)) return;
            if (context.Request.Path.StartsWith("/scripts/", StringComparison.OrdinalIgnoreCase)) return;

            using (MiniProfiler.Current.Step("Resolve route"))
            {
                base.PostResolveRequestCache(context);
            }
        }

    }
}