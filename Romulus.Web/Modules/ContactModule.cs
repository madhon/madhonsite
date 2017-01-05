namespace Romulus.Web.Modules
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MediatR;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;
    using Romulus.Web.ViewModels;

    [UsedImplicitly]
    public class ContactModule : BaseModule
    {
        private readonly IMediator mediator;

        public ContactModule(IMediator mediator)
        {
            this.mediator = mediator;
            Get["/contact"] = _ => GetIndex();

            Post["/contact", true] = async (x, ct) =>
            {
                try
                {
                    this.ValidateCsrfToken();
                }
                catch (CsrfValidationException)
                {
                    return View["Views/Contact/Index"].WithStatusCode(HttpStatusCode.Forbidden);
                }

                var cvm = this.BindAndValidate<ContactViewModel>();
                if (ModelValidationResult.IsValid)
                {
                    await this.mediator.Publish(cvm).WithoutCapturingContext();
                    return GetComplete();
                }

                Page.Title = "Contact";
                return View["Views/Contact/Index", Model];
            };

            Get["/contact/complete"] = _ => GetComplete();
        }

        private dynamic GetIndex()
        {
            this.CreateNewCsrfToken();
            Page.Title = "Contact";
            return View["Views/Contact/Index", Model];
        }

        private dynamic GetComplete()
        {
            Page.Title = "Contact";
            return View["Views/Contact/Complete", Model];
        }
    }
}