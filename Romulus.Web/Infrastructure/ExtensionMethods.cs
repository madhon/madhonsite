using System;
using System.Collections.Generic;
using System.Linq;

namespace Romulus.Web
{
    public static class ExtensionMethods
    {
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}