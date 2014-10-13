using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Romulus.Web.Services;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Modules
{
    public class ContactModule : NancyModule
    {
        private readonly IContactService contactService;

        public ContactModule(IContactService contactService)
        {
            this.contactService = contactService;
            Get["/contact"] = _ => GetIndex(_);

            Post["/contact", true] = async (x, ct) =>
            {
                var model = this.Bind<ContactViewModel>();
                var validationResult = this.Validate(model);
                if (validationResult.IsValid)
                {
                    await SendMessageAsync(model);
                    return View["Views/Contact/Complete"];
                }

                return View["Views/Contact/Index", model];
            };

            Get["/contact/complete"] = _ => GetComplete(_);
        }

        private dynamic GetIndex(dynamic p)
        {
            ViewBag.title = "Contact";
            return View["Views/Contact/Index"];
        }

        private dynamic GetComplete(dynamic p)
        {
            ViewBag.title = "Contact";
            return View["Views/Contact/Complete"];
        }

       private async Task SendMessageAsync(ContactViewModel model)
        {
            await contactService.SendMessageAsync(model);
        }
    }
}