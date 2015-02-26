namespace Romulus.Web.ViewModels
{
    using FluentValidation;

    public class ContactViewModelValidator : AbstractValidator<ContactViewModel>
    {
        public ContactViewModelValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Enter your name");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Enter a valid email address");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Enter a valid email address");
            RuleFor(c => c.Message).NotEmpty().WithMessage("Enter your message");
        }
    }
}