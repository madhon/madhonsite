using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Services
{
    public class ContactService : IContactService
    {
        public void SendMessage(ContactViewModel model)
        {
            MailMessage message = new MailMessage
                                      {
                                          From = new MailAddress(model.Email, model.Name),
                                          Subject = "Message from website",
                                          Sender = new MailAddress(model.Email, model.Name),
                                          IsBodyHtml = false,
                                          Body = model.Message
                                      };

            message.To.Add(new MailAddress("madhon@madhon.com", "Madhon"));
            SmtpClient client = new SmtpClient {Host = "ASPMX.L.GOOGLE.com", Port = 25};
            
            try
            {
                client.Send(message);
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                message.Dispose();
            }
        }
    }
}