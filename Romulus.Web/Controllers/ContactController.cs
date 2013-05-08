using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Romulus.Web.Services;
using Romulus.Web.ViewModels;
using StackExchange.Exceptional;

namespace Romulus.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await SendMessageAsync(model);
                    return RedirectToAction("Complete");
                }
                catch (Exception ex)
                {
                    ErrorStore.LogException(ex, System.Web.HttpContext.Current);
                }            
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Complete()
        {
            return View();
        }

        private async Task SendMessageAsync(ContactViewModel model)
        {
            await contactService.SendMessageAsync(model);
        }

        private void SendMessage(ContactViewModel model)
        {
            contactService.SendMessage(model);
        }
    }
}
