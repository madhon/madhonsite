namespace Romulus.Web.Services
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MimeKit;
    using Romulus.Web.ViewModels;
    
    public class ContactService : IContactService
    {
        private ITransport transport;

        public ContactService(ITransport transport)
        {
            this.transport = transport;
        }

        public async Task SendMessageAsync([NotNull] ContactViewModel model)
        {
            var message = this.CreateMailMessage(model);
            await this.transport.DeliverAsync(message).WithoutCapturingContext();
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
    }
}