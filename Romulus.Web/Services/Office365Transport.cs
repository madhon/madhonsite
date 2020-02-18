namespace Romulus.Web.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;

    public class Office365Transport : ITransport
    {
        public async Task DeliverAsync(MimeMessage message, CancellationToken ct)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("madhon-co-uk.mail.protection.outlook.com", 25, SecureSocketOptions.StartTls, ct).WithoutCapturingContext();
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.SendAsync(message, ct).WithoutCapturingContext();
                await client.DisconnectAsync(true, ct).WithoutCapturingContext();
            }
        }
    }
}
