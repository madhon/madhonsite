namespace Romulus.Web.ViewModels
{
  using System.ComponentModel.DataAnnotations;
  using JetBrains.Annotations;
  using MediatR;

  [UsedImplicitly]
  public class ContactViewModel : INotification
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Your Name")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Your Name")]
    public string Message { get; set; }

  }
}
