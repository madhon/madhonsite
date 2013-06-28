using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HtmlTags;

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

        public static HtmlTag BootstrapLabelFor(this HtmlHelper html, string name)
        {
            return new HtmlTag("label").Id(name).AddClass("control-label").Attr("for", name).Text(name.ToTitleCase());
        }
    }
}