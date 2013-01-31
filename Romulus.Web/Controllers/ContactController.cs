using System;
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
        public ActionResult Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contactService.SendMessage(model);
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

    }
}
