namespace Romulus.Web.Services
{
    public class Office365Transport : ITransport
    {
        public async Task DeliverAsync(MimeMessage message, CancellationToken ct)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("madhon-co-uk.mail.protection.outlook.com", 25, SecureSocketOptions.StartTls, ct).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.SendAsync(message, ct).ConfigureAwait(false);
                await client.DisconnectAsync(true, ct).ConfigureAwait(false);
            }
        }
    }
}
