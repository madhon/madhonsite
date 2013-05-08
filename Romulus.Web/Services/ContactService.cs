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
        public async Task SendMessageAsync(ContactViewModel model)
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

            await SendEmailAwaitable(message: message, server: "ASPMX.L.GOOGLE.com", port: 25);
        }

        public void SendMessage(ContactViewModel model)
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
            SmtpClient client = new SmtpClient { Host = "ASPMX.L.GOOGLE.com", Port = 25 };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                client.Dispose();
                message.Dispose();
            }
        }

        private async Task<bool> SendEmailAwaitable(MailMessage message, string server, int port)
        {
            SmtpClient smtpClient = new SmtpClient(server, port);
            smtpClient.Send(message);
            await Task.Yield();
            return true;
        }
    }
}