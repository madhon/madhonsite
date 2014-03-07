using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Romulus.Web.Infrastructure;
using StackExchange.Exceptional;
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

 		public static DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        public static Controller Controller
        {
            get { return GetFromContext<Controller>("Controller"); }
            set { SetInContext("Controller", value); }
        }

        public static MiniProfiler Profiler
        {
            get { return MiniProfiler.Current; }
        }

        public static bool IsAdmin
        {
            get
            {
                var allowedIpAddresses = new Dictionary<string, string>
                {
                    {"93.96.173.32", "Home"},
                    {"89.213.243.64", "AlphaLogix Not Demon"},
                    {"89.243.253.162", "AlphaLogix TalkTalk"}
                };


                var ip = Request.GetClientIpAddress();
                if (Request.IsLocal)
                {
                    return true;
                }

                if (allowedIpAddresses.ContainsKey(ip))
                {
                    return true;
                }

                return false;
            }
        }

		public static string RemoteIP
        {
            get
            {
                var serverVars = HttpContext.Current.Request.ServerVariables;
                var headers = HttpContext.Current.Request.Headers;
                return GetRemoteIP(headers["X-Real-IP"], serverVars["REMOTE_ADDR"], headers["X-Forwarded-For"]);
            }
        }

        private static readonly Regex LastAddress = new Regex(@"\b(\d|a-f|\.|:)+$", RegexOptions.Compiled);

        public static string GetRemoteIP(string realIp, string remoteAddr, string xForwardedFor)
        {
            // if we've got an authoritative ip, use it
            if (realIp.HasValue() && !IsPrivateIP(realIp))
            {
                return realIp;
            }

            if (xForwardedFor.HasValue())
            {
                xForwardedFor = LastAddress.Match(xForwardedFor).Value;
                if (xForwardedFor.HasValue() && !IsPrivateIP(xForwardedFor))
                    remoteAddr = xForwardedFor;
            }

            if (!remoteAddr.HasValue()) throw new Exception("Cannot determine source of request");
            return remoteAddr;
        }

        internal static bool IsPrivateIP(string s)
        {
            var ipv4Check = (s.StartsWith("192.168.") || s.StartsWith("10.") || s.StartsWith("127.0.0."));
            if (ipv4Check) return true;
            IPAddress addr;
            
            if(!IPAddress.TryParse(s, out addr) || addr.AddressFamily != AddressFamily.InterNetworkV6) return false;
            // IPv6 reserves fc00::/7 for local usage
            // http://en.wikipedia.org/wiki/Unique_local_address
            var address = addr.GetAddressBytes();
            return address[0] == (byte)0xFD;    //FC + the L-bit set to make FD
        }

        public static T GetFromCache<T>(string name) where T : class
        {
            return HttpRuntime.Cache[name] as T;
        }

        public static void AddToCache<T>(string name, T o, TimeSpan expiresIn) where T : class
        {
 			HttpRuntime.Cache.Insert(name, o, null, Current.Now + expiresIn, Cache.NoSlidingExpiration);
        }

		public static bool RemoveFromCache(string name)
		{
			var oldValue = HttpRuntime.Cache.Remove(name);
			return oldValue != null;
		}

       private static T GetFromContext<T>(string name) where T : class
        {
            return HttpContext.Current.Items[name] as T;
        }

 		private static void SetInContext<T>(string name, T value) where T : class
        {
            HttpContext.Current.Items[name] = value;
        }

        public static void LogException(Exception e)
        {
            ErrorStore.LogException(e, Context);
        }
    }
}