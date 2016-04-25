namespace Romulus.Web.Modules
{
  using System;
  using System.Threading.Tasks;
  using JetBrains.Annotations;
  using Nancy;
  using Nancy.ModelBinding;
  using Nancy.Security;
  using Romulus.Web.Services;
  using Romulus.Web.ViewModels;

  [UsedImplicitly]
  public class ContactModule : NancyModule
  {
    private readonly IContactService contactService;

    public ContactModule(IContactService contactService)
    {
      this.contactService = contactService;
      Get["/contact"] = _ => GetIndex(_);

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

        var model = this.BindAndValidate<ContactViewModel>();
        if (ModelValidationResult.IsValid)
        {
          await SendMessageAsync(model).WithoutCapturingContext();
          return View["Views/Contact/Complete"];
        }

        return View["Views/Contact/Index", model].WithStatusCode(HttpStatusCode.BadRequest);
      };

      Get["/contact/complete"] = _ => GetComplete(_);
    }

    private dynamic GetIndex(dynamic p)
    {
      this.CreateNewCsrfToken();
      ViewBag.title = "Contact";
      return View["Views/Contact/Index"].WithStatusCode(HttpStatusCode.OK);
    }

    private dynamic GetComplete(dynamic p)
    {
      ViewBag.title = "Contact";
      return View["Views/Contact/Complete"].WithStatusCode(HttpStatusCode.OK);
    }

    private async Task SendMessageAsync([NotNull] ContactViewModel model) => await contactService.SendMessageAsync(model).WithoutCapturingContext();
  }
}
