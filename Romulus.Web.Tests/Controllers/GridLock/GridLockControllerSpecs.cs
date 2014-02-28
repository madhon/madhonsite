using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Romulus.Web.Controllers;
using Xbehave;

namespace Romulus.Web.Tests.Controllers.GridLock
{
    public class GridLockControllerSpecs
    {
        private GridlockController controller;
        private ActionResult viewResult;

        [Scenario]
        public void ShouldReturnEmptyViewResult()
        {
            "Given a Gridlock Controller".Given(() => controller = new GridlockController());
            "When The Index action is called".When(() => viewResult = (ViewResult)controller.Index());
            "Then It should be a ViewResult object".Then(() => viewResult.Should().BeAssignableTo<ViewResult>());
            "And it should be empty".Then(() =>
            {
                var vr = viewResult as ViewResult;
                vr.ViewName.Should().BeEmpty();
            });
        }
    }
}
