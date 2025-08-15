namespace Romulus.Web.Services;

using System.Threading;
using System.Threading.Tasks;
using MimeKit;

internal sealed class NullTransport : ITransport
{
    public async Task DeliverAsync(MimeMessage message, CancellationToken ct)
    {
        await Task.Delay(2, ct).ConfigureAwait(false);
    }
}
