using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using Machine.Specifications;
using Romulus.Web.Controllers;
using Romulus.Web.Services;
using Romulus.Web.ViewModels;

namespace Romulus.Web.Tests.Controllers.Contact
{
    [Subject(typeof (ContactController))]
    internal class when_index_action_with_model_is_invoked
    {
        private static ContactController controller;
        private static Task<ActionResult> viewResult;
        private static IContactService service;
        private static ContactViewModel goodModel;
        private static ContactViewModel badModel;

        Establish context = () =>
            {
                service = new ContactService();
                controller = new ContactController(service);

                goodModel = new ContactViewModel
                    {
                        Email = "madhon@madhon.co.uk",
                        Name = "Madhon",
                        Message = "Spec Testing"
                    };
            };

        Because of = () => viewResult = controller.Index(goodModel);

        It should_return_a_ViewResult_object = () => viewResult.Should().BeAssignableTo<Task<ActionResult>>();
    }
}