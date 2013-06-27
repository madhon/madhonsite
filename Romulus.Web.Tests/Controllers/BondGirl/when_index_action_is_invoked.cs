using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Machine.Specifications;
using Should.Fluent;
using Romulus.Web.Controllers;

namespace Romulus.Web.Tests.Controllers.BondGirl
{
    [Subject(typeof(BondGirlController))]
    public class when_index_action_is_invoked
    {
        static BondGirlController controller;
        static ActionResult viewResult;

        Establish context = () => controller = new BondGirlController();

        Because of = () => viewResult = (ViewResult)controller.Index();

        It should_return_a_viewresult_object = () => viewResult.Should().Be.OfType<ViewResult>();

        It should_return_a_viewresult_with_default_view_name = () =>
        {
            var vr = viewResult as ViewResult;
            vr.ViewName.Should().Be.Empty();
        };

    }
}
