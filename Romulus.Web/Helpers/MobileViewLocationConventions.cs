namespace Romulus.Web.Helpers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Nancy;
    using Nancy.Conventions;

    public static class MobileViewLocationConventions
    {
        /// <summary>
        /// Sets up a view convention to support mobile specific views. When the requesting user agent
        /// is a mobile device, the "-mobile" suffix will be added to the candidate view name.
        /// </summary>
        /// <param name="conventions"></param>
        public static void EnableMobileViewLocationConventions(this NancyConventions conventions)
        {
            // Take the existing list of view conventions, and reverse it
            var existingConventions = conventions.ViewLocationConventions.ToList();
            existingConventions.Reverse();

            // Wrap all the existing conventions in a func that returns the existing viewname with -mobile suffix
            foreach (var existingConvention in existingConventions)
            {
                conventions.ViewLocationConventions.Insert(0, (viewname, model, context) =>
                {
                    if (!context.Context.Request.IsMobile())
                    {
                        return String.Empty;
                    }

                    var oldViewName = existingConvention(viewname, model, context);

                    if (oldViewName == null)
                    {
                        return String.Empty;
                    }

                    return string.Format("{0}-mobile", oldViewName);
                });
            }
        }

        public static bool IsMobile(this Request request)
        {
            string userAgent = request.Headers.UserAgent;

            var defaultRegex = "/Mobile|iP(hone|od|ad)|Android|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune/";

            var regex = ConfigurationManager.AppSettings["Nancy.MobileViewLocationConventions.MobileUserAgentRegex"];

            if (string.IsNullOrEmpty(regex))
            {
                regex = defaultRegex;
            }

            return Regex.IsMatch(userAgent, regex, RegexOptions.IgnoreCase);
        }
    }
}