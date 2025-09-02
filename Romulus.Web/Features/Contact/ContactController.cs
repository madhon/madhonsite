namespace Romulus.Web.Features.Contact;

public sealed class ContactController : Controller
{
    private readonly IMediator mediator;

    private readonly IValidator<Send.Command> commandValidator;
    public ContactController(IMediator mediator, IValidator<Send.Command> commandValidator)
    {
        this.mediator = mediator;
        this.commandValidator = commandValidator;
    }

    [HttpGet]
    [AllowAnonymous()]
    public IActionResult Index() => View();

    [HttpPost]
    [AllowAnonymous()]
    public async Task<IActionResult> Index(Send.Command command, CancellationToken cancellationToken)
    {
        using (var act = InstrumentationConfig.ActivitySource.StartActivity("ContactController.Index", ActivityKind.Internal))
        {
            var validationResult = await commandValidator.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View("Index");
            }

            await mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return View("Complete");
        }
    }
}
