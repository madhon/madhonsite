using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Romulus.Web.Controllers;
using Xunit;

namespace Romulus.Web.Tests.Controllers
{
    public class HomeControllerFacts
    {
        public class Index
        {
            [Fact]
            public void ReturnsViewResultWithDefaultViewName()
            {
                var controller = new HomeController();
                var result = controller.Index();

                result.Should().BeOfType<ViewResult>();
                
                var viewResult = result.As<ViewResult>();
                viewResult.ViewName.Should().NotBeNullOrEmpty();
            }
        }
    }
}