namespace Romulus.Web.Features.Contact;

public class ContactController : Controller
{
    private readonly IMediator mediator;

    public ContactController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    [AllowAnonymous()]
    public IActionResult Index() => View();

    [HttpPost]
    [AllowAnonymous()]
    public async Task<IActionResult> Index(Send.Command command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        await mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return View("Complete");
    }
}
