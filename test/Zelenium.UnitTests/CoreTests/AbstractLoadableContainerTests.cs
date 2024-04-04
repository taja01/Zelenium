using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.WebDriver.Types;

namespace Zelenium.UnitTests.CoreTests
{
    [TestFixture]
    class AbstractLoadableContainerTests
    {
        [Test]
        public void PassedTest()
        {
            var driver = new Mock<IWebDriver>();
            var logger = new Mock<ILogger<AbstractLoadableContainerTests>>();

            driver.As<IJavaScriptExecutor>();
            var mockContainer = new Mock<AbstractLoadableContainer>(logger.Object, driver.Object, By.Id("id"), "url") { };
            mockContainer.Setup(d => d.IsLoaded()).Returns(new Core.Model.ValidationResult { Passed = true });

            var mockLoadableContainer = mockContainer.Object;

            Assert.That(mockLoadableContainer.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void IsLoadedTest()
        {
            var driver = new Mock<IWebDriver>();
            var logger = new Mock<ILogger<AbstractLoadableContainerTests>>();

            driver.As<IJavaScriptExecutor>();
            var mock = new Mock<AbstractLoadableContainer>(logger.Object, driver.Object, By.CssSelector(".class"), "url") { };

            mock.Setup(d => d.IsLoaded()).Returns(new Core.Model.ValidationResult { Passed = false, Message = "Title not found" });

            var mockLoadableContainer = mock.Object;

            Assert.That(mockLoadableContainer.IsLoaded().Passed, Is.False);
            Assert.That(mockLoadableContainer.IsLoaded().Message, Is.EqualTo("Title not found"));
        }

        [Test]
        public void LoadTest()
        {
            //wut?
            var driver = new Mock<IWebDriver>();
            var logger = new Mock<ILogger<AbstractLoadableContainerTests>>();

            driver.Setup(x => x.Url).Returns("https://google.com");
            driver.As<IJavaScriptExecutor>();
            var mock = new Mock<AbstractLoadableContainer>(logger.Object, driver.Object, By.CssSelector(".class"), "https://google.com") { };

            var mockLoadableContainer = mock.Object;
            mockLoadableContainer.Load();

            Assert.That(driver.Object.Url, Does.Contain("https://google.com"));
        }
    }
}
