using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Machine.Specifications;
using Romulus.Web.Controllers;
using Romulus.Web.Services;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Tests.Controllers.Contact
{
    [Subject(typeof(ContactController))]
    public class when_index_action_is_invoked
    {
        static ContactController controller;
        static ActionResult viewResult;
        static IContactService service;

        Establish context = () =>
            {
                service = new ContactService();
                controller = new ContactController(service);
            };

        Because of = () => viewResult = (ViewResult)controller.Index();

        It should_return_a_ViewResult_object = () => viewResult.ShouldBeOfType<ViewResult>();

        It should_return_a_ViewResult_with_default_view_name = () =>
        {
            var vr = viewResult.As<ViewResult>();
            vr.ViewName.Should().BeEmpty();
        };
    }
}
