namespace Romulus.Web.Infrastructure;

using System.Collections.Generic;
using static Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults;

internal static class ResponseCompressionMimeTypes
{
    public static IReadOnlyList<string> Defaults { get; } =
    [
        .. MimeTypes,
        "application/atom+xml",
        "image/svg+xml",
        "image/x-icon",
        "application/vnd.ms-fontobject",
        "application/x-font-ttf",
        "font/otf",
    ];
}
