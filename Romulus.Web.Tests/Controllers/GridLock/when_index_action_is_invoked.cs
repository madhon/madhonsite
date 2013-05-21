using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Machine.Specifications;
using Romulus.Web.Controllers;

namespace Romulus.Web.Tests.Controllers.GridLock
{
    [Subject(typeof(GridlockController))]
    public class when_index_action_is_invoked
    {
        static GridlockController controller;
        static ActionResult viewResult;

        Establish context = () => controller = new GridlockController();

        Because of = () => viewResult = (ViewResult)controller.Index();

        It should_return_a_ViewResult_object = () => viewResult.ShouldBeOfType<ViewResult>();

        It should_return_a_ViewResult_with_default_view_name = () =>
        {
            var vr = viewResult.As<ViewResult>();
            vr.ViewName.Should().BeEmpty();
        };
    }
}
