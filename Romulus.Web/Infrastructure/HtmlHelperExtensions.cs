using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Romulus.Web.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString AssemblyVersion(this HtmlHelper helper)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return MvcHtmlString.Create(version);
        }

        public static IHtmlString AssemblyFileVersion(this HtmlHelper helper)
        {
            AssemblyFileVersionAttribute attr = typeof(HtmlHelperExtensions).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).OfType<AssemblyFileVersionAttribute>().FirstOrDefault();
            return MvcHtmlString.Create(attr != null ? attr.Version : string.Empty);
        }
    }
}