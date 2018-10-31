namespace Romulus.Web.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MediatR;
    using MimeKit;
    using Romulus.Web.ViewModels;

    public class ContactMessageHandler : INotificationHandler<ContactViewModel>
    {
        private readonly ITransport transport;

        public ContactMessageHandler(ITransport transport)
        {
            this.transport = transport;
        }

        public async Task Handle(ContactViewModel notification, CancellationToken ct)
        {
            var message = CreateMailMessage(notification);
            await transport.DeliverAsync(message, ct).WithoutCapturingContext();
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
