using ASC.Tests.TestUtilities;
using ASC.Utilities;
using ASC_Web.Configuration;
using ASC_Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASC.Tests
{

    public class HomeControllerTests
    {

         private readonly Mock<IOptions<ApplicationSetting>> optionsMock;
        private readonly Mock<HttpContext> mockHttpContext;
        private readonly Mock<ILogger<HomeController>> loggerMock;

        public HomeControllerTests()
        {
            // Create an instance of Mock IOptions
            optionsMock = new Mock<IOptions<ApplicationSetting>>();
            mockHttpContext = new Mock<HttpContext>();
            loggerMock = new Mock<ILogger<HomeController>>();

            // Set FakeSession to HttpContext Session.
            mockHttpContext.Setup(p => p.Session).Returns(new FakeSession());

            // Set IOptions<> Values property to return ApplicationSetting object
            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSetting
            {
                ApplicationTitle = "ASC"
            });
        }

        [Fact]
        public void HomeController_Index_View_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void HomeController_Index_NoModel_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            Assert.Null((controller.Index() as ViewResult).ViewData.Model);
        }

        [Fact]
        public void HomeController_Index_Validation_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            Assert.Equal(0, (controller.Index() as ViewResult).ViewData.ModelState.ErrorCount);
        }
        [Fact]
        public void HomeController_Index_Session_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            controller.Index();
            //Session value with key "Test" should not be null.
            Assert.NotNull(controller.HttpContext.Session.GetSession<ApplicationSetting>("Test"));
        }


    }
}
