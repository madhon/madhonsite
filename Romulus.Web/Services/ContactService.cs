namespace Romulus.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MailKit.Net.Smtp;
    using MimeKit;
    using Romulus.Web.ViewModels;
    
    public class ContactService : IContactService
    {
        public async Task SendMessageAsync([NotNull] ContactViewModel model)
        {
            var message = this.CreateMailMessage(model);
            await this.SendEmailTask(message).WithoutCapturingContext();
        }

        private MimeMessage CreateMailMessage([NotNull] ContactViewModel model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(model.Name, model.Email));
            message.To.Add(new MailboxAddress("Madhon", "madhon@madhon.com"));
            message.Subject = "Message from website";
            message.Body = new TextPart("plain") { Text = model.Message };

            message.Headers.Add(new Header("X-Generator", "MimeKit"));

            return message;
        }

        private async Task SendEmailTask([NotNull] MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("ASPMX.L.GOOGLE.com", 25).WithoutCapturingContext();

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.SendAsync(message).WithoutCapturingContext();
                await client.DisconnectAsync(true).WithoutCapturingContext();
            }
        }
    }
}