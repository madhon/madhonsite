using System;
using System.Web.Optimization;

namespace Romulus.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ddlevelsmenu").Include(
                        "~/Scripts/ddlevelsmenu.js"));

            bundles.Add(new StyleBundle("~/bundles/metrocss")
                .Include("~/Content/ddlevelsmenu-base.css")
                .Include("~/Content/ddlevelsmenu-topbar.css")                
                .Include("~/Content/metro.css"));
   
        }
    }
}