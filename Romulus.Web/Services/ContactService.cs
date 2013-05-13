using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Services
{
    public class ContactService : IContactService
    {
        public void SendMessage(ContactViewModel model)
        {
            using (var mailMessage = CreateMailMessage(model: model))
            using (var smtp = CreateSmtpClient())
            {
                smtp.Send(mailMessage);
            }
        }

        public async Task SendMessageAsync(ContactViewModel model)
        {
            var message = CreateMailMessage(model: model);
            await SendEmailAwaitable(message: message);
        }

        private async Task SendEmailAwaitable(MailMessage message)
        {
            SmtpClient smtpClient = CreateSmtpClient();
            smtpClient.Send(message);
            await Task.Yield();
        }

        private MailMessage CreateMailMessage(ContactViewModel model)
        {
            MailMessage message = new MailMessage
                {
                    From = new MailAddress(address: model.Email, displayName: model.Name),
                    Subject = "Message from website",
                    Sender = new MailAddress(address: model.Email, displayName: model.Name),
                    IsBodyHtml = false,
                    Body = model.Message
                };

            message.To.Add(new MailAddress("madhon@madhon.com", "Madhon"));
            return message;
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient {Host = "ASPMX.L.GOOGLE.com", Port = 25};
        }
    }
}