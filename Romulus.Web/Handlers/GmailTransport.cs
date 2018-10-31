namespace Romulus.Web.Handlers
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using JetBrains.Annotations;
  using MailKit.Net.Smtp;
  using MimeKit;

  [UsedImplicitly]
  public class GmailTransport : ITransport
  {
    public async Task DeliverAsync([NotNull] MimeMessage message, CancellationToken ct)
    {
      using (var client = new SmtpClient())
      {
        await client.ConnectAsync("ASPMX.L.GOOGLE.com", 25, cancellationToken: ct).WithoutCapturingContext();

        // Note: since we don't have an OAuth2 token, disable
        // the XOAUTH2 authentication mechanism.
        client.AuthenticationMechanisms.Remove("XOAUTH2");

        await client.SendAsync(message, ct).WithoutCapturingContext();
        await client.DisconnectAsync(true, ct).WithoutCapturingContext();
      }
    }
  }
}
