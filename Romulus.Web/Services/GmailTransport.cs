namespace Romulus.Web.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;
    using MimeKit;

    public class GmailTransport : ITransport
    {
        public async Task DeliverAsync(MimeMessage message, CancellationToken ct)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("ASPMX.L.GOOGLE.COM", 25, cancellationToken: ct).WithoutCapturingContext();
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.SendAsync(message, ct).WithoutCapturingContext();
                await client.DisconnectAsync(true, ct).WithoutCapturingContext();
            }
        }
    }
}
