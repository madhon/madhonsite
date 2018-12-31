namespace Romulus.Web.Features.Contact
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using JetBrains.Annotations;
    using MediatR;
    using MimeKit;
    using Romulus.Web.Services;

    public class Send
    {
        public class Command : IRequest, INotification
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("Enter your name");
                RuleFor(c => c.Email).NotEmpty().WithMessage("Enter a valid email address");
                RuleFor(c => c.Email).EmailAddress().WithMessage("Enter a valid email address");
                RuleFor(c => c.Message).NotEmpty().WithMessage("Enter your message");
            }
        }

        public class Handler : INotificationHandler<Command>
        {
            private readonly ITransport transport;

            public Handler(ITransport transport)
            {
                this.transport = transport;
            }

            private MimeMessage CreateMailMessage([NotNull] Command model)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(model.Name, model.Email));
                message.To.Add(new MailboxAddress("Madhon", "madhon@madhon.com"));
                message.Subject = "Message from website";
                message.Body = new TextPart("plain") { Text = model.Message };

                message.Headers.Add(new Header("X-Generator", "MimeKit"));

                return message;
            }

            public async Task Handle(Command notification, CancellationToken ct)
            {
                var message = CreateMailMessage(notification);
                await transport.DeliverAsync(message, ct).WithoutCapturingContext();
            }
        }
    }
}