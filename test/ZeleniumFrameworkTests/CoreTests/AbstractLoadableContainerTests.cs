using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using ZeleniumFramework.WebDriver;

namespace ZeleniumFrameworkTest.CoreTests
{
    [TestFixture]
    class AbstractLoadableContainerTests
    {
        [Test]
        public void PassedTest()
        {
            var driver = new Mock<IWebDriver>();
            var mock = new Mock<AbstractLoadableContainer>(driver.Object, By.Id("id"), "url")
            {
                CallBase = true
            };
            mock.Setup(d => d.IsLoaded()).Returns(new ZeleniumFramework.Model.ValidationResult { Passed = true });

            var mockLoadableContainer = mock.Object;

            Assert.IsTrue(mockLoadableContainer.IsLoaded().Passed);
        }

        [Test]
        public void IsLoadedTest()
        {
            var driver = new Mock<IWebDriver>();
            var mock = new Mock<AbstractLoadableContainer>(driver.Object, By.CssSelector(".class"), "url")
            {
                CallBase = true
            };

            mock.Setup(d => d.IsLoaded()).Returns(new ZeleniumFramework.Model.ValidationResult { Passed = false, Message = "Title not found" });

            var mockLoadableContainer = mock.Object;

            Assert.IsFalse(mockLoadableContainer.IsLoaded().Passed);
            Assert.AreEqual("Title not found", mockLoadableContainer.IsLoaded().Message);
        }

        [Test]
        public void LoadTest()
        {
            var driver = new Mock<IWebDriver>();
            driver.Setup(x => x.Url).Returns("https://google.com");
            var mock = new Mock<AbstractLoadableContainer>(driver.Object, By.CssSelector(".class"), "https://google.com")
            {
            };

            var mockLoadableContainer = mock.Object;
            mockLoadableContainer.Load();

            Assert.AreEqual(driver.Object.Url, "https://google.com");
        }
    }
}
