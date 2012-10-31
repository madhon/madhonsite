using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FluentAssertions;
using Machine.Specifications;
using Romulus.Web.Controllers;

namespace Romulus.Web.Tests.Controllers.Home
{
    [Subject(typeof (HomeController))]
    public class when_index_action_is_invoked
    {
        private static HomeController controller;
        private static ViewResult viewResult;

        private Establish context = () => controller = new HomeController();

        private Because of = () => viewResult = (ViewResult) controller.Index();

        private It should_return_a_ViewResult_with_default_view_name = () => viewResult.ViewName.Should().BeEmpty();
    }
}