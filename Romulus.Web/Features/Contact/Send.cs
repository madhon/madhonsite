namespace Romulus.Web.Features.Contact;

public class Send
{
    public record Command : ICommand
    {
        public string Name { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Message { get; init; } = default!;
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

    public class Handler : ICommandHandler<Command>
    {
        private readonly ITransport transport;

        public Handler(ITransport transport) => this.transport = transport;

        private MimeMessage CreateMailMessage(Command model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(model.Name, model.Email));
            message.To.Add(new MailboxAddress("Madhon", "madhon@madhon.com"));
            message.Subject = "Message from website";
            message.Body = new TextPart("plain") { Text = model.Message };

            message.Headers.Add(new Header("X-Generator", "MimeKit"));

            return message;
        }

        public async ValueTask<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var message = CreateMailMessage(request);
            await transport.DeliverAsync(message, cancellationToken).ConfigureAwait(false);
            return new Unit();
        }
    }
}
