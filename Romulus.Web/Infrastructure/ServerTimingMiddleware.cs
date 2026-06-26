namespace Romulus.Web.Infrastructure;

using System.Globalization;

internal sealed class ServerTimingMiddleware : IMiddleware
{
    private const string ServerTimingHttpHeader = "Server-Timing";

    /// <inheritdoc/>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        if (context.Response.SupportsTrailers())
        {
            context.Response.DeclareTrailer(ServerTimingHttpHeader);
            var start = Stopwatch.GetTimestamp();

            await next(context).ConfigureAwait(false);

            var elapsed = Stopwatch.GetElapsedTime(start);

            context.Response.AppendTrailer(
                ServerTimingHttpHeader,
                $"app;dur={elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}");

        }
        else
        {
            await next(context).ConfigureAwait(false);
        }
    }
}
