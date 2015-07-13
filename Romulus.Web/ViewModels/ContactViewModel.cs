namespace Romulus.Web.ViewModels
{
  using System;
  using JetBrains.Annotations;

  [UsedImplicitly]
  public class ContactViewModel
  {
    public string Name { get; set; }

    public string Email { get; set; }

    public string Message { get; set; }
  }
}
