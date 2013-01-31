using System;
using System.Web.Mvc;
using Romulus.Web.Infrastructure;

namespace Romulus.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ProfilingActionFilter());
        }
    }
}