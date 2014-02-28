using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Romulus.Web.ViewModels;
using Xunit;

namespace Romulus.Web.Tests.Validators
{
    public class ContactValidatorTests
    {
        private readonly ContactViewModelValidator validator;

        public ContactValidatorTests()
        {
            validator = new ContactViewModelValidator();
        }

        [Fact]
        public void ShouldNotHaveValidationErrorsWhenValidModelIsSupplied()
        {
            ContactViewModel model = new ContactViewModel
            {
                Name = "jon",
                Email = "madhon@madhon.co.uk",
                Message = "Lorem Ipsum"
            };

            ValidationResult result = validator.Validate(model);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotHaveValidationErrorWhenNameIsSupplied()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, "name");
        }

        [Fact]
        public void ShouldHaveValidationErrorWhenNoNameIsSupplied()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, (string)null);
        }

        [Fact]
        public void ShouldNotHaveValidationErrorWhenMessageIsSupplied()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Message, "message");
        }

        [Fact]
        public void ShouldHaveValidationErrorWhenNoMessageIsSupplied()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Message, (string)null);
        }

        [Fact]
        public void ShouldNotHaveValidationErrorWhenValidEmailIsSupplied()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, "test@test.com");
        }

        [Fact]
        public void ShouldHaveValidationErrorWhenNoEmailIsSupplied()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, (string)null);
        }

        [Fact]
        public void ShouldHaveValidationErrorWhenInvalidEmailIsSupplied()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "test@");
        }

    }
}