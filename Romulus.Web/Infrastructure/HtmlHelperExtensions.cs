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
            AssemblyInformationalVersionAttribute attr = typeof(HtmlHelperExtensions).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true).OfType<AssemblyInformationalVersionAttribute>().FirstOrDefault();
            return MvcHtmlString.Create(attr != null ? attr.InformationalVersion : string.Empty);
        }
    }
}