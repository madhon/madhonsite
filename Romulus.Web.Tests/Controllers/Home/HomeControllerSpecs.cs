using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Romulus.Web.Controllers;
using Should.Fluent;
using Xbehave;

namespace Romulus.Web.Tests.Controllers.Home
{
    public class HomeControllerSpecs
    {
        private HomeController controller;
        private ActionResult viewResult;

        [Scenario]
        public void ShouldReturnEmptyViewResult()
        {
            "Given a Home Controller".Given(() => controller = new HomeController());
            "When The Index action is called".When(() => viewResult = (ViewResult) controller.Index());
            "Then It should be a ViewResult object".Then(() => viewResult.Should().Be.OfType<ViewResult>());
            "And it should be empty".Then(() =>
                {
                    var vr = viewResult as ViewResult;
                    vr.ViewName.Should().Be.Empty();
                });
        }
    }
}