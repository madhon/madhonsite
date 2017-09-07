namespace Romulus.Web.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using JetBrains.Annotations;
  using MediatR;

  [UsedImplicitly]
  public class ContactViewModel : INotification, IValidatableObject
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Your Name")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Your Name")]
    public string Message { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }
  }
}
