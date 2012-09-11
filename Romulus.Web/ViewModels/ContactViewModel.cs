using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Attributes;


namespace Romulus.Web.ViewModels
{
    [Validator(typeof(ContactViewModelValidator))]
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}