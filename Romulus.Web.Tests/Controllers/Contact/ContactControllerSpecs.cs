using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Romulus.Web.Controllers;
using Romulus.Web.Services;
using Xbehave;

namespace Romulus.Web.Tests.Controllers.Contact
{
    public class ContactControllerSpecs
    {
        private ContactController controller;
        private ActionResult viewResult;

        [Scenario]
        public void IndexShouldReturnEmptyViewResult()
        {
            "Given a Contact Controller".Given(() => controller = new ContactController(new ContactService()));
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
