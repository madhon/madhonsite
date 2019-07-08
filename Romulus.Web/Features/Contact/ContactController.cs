namespace Romulus.Web.Features.Contact
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : Controller
    {
        private readonly IMediator mediator;

        public ContactController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Send.Command command)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            await mediator.Publish(command).WithoutCapturingContext();
            return View("Complete");
        }
    }
}
