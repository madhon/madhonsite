namespace Romulus.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using static Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults;

    public static class ResponseCompressionMimeTypes
    {
        public static IEnumerable<string> Defaults
            => MimeTypes.Concat(new[]
            {
                // ATOM
                "application/atom+xml",
                // Images
                "image/svg+xml",
                "image/x-icon",
                // Fonts
                "application/vnd.ms-fontobject",
                "application/x-font-ttf",
                "font/otf"
            });
    }
}
