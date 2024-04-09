﻿using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Interfaces;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace Zelenium.UnitTests.CoreTests
{
    [TestFixture]
    internal class RouteBuilderTests
    {
        private ConcreteLoadableContainer _container;

        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger>();
            var webDriver = new Mock<IWebDriver>();
            webDriver.As<IJavaScriptExecutor>();
            var locator = By.Id("test");
            var routeBuilder = new Mock<IRouteBuilder<MyPage>>();
            routeBuilder.Setup(t => t.GetUrl(MyPage.MainPage)).Returns("http://test.com");

            _container = new ConcreteLoadableContainer(logger.Object, webDriver.Object, locator, routeBuilder.Object, MyPage.MainPage);
        }

        [Test]
        public void TestThatUrlIsCorrect()
        {
            Assert.That("http://test.com", Is.EqualTo(_container.Url));
        }
    }

    public enum MyPage
    {
        MainPage
    }

    public class ConcreteLoadableContainer : AbstractLoadableContainer<MyPage>
    {
        public ConcreteLoadableContainer(ILogger logger, IWebDriver webDriver, By locator, IRouteBuilder<MyPage> routeBuilder, MyPage page) :
            base(logger, webDriver, locator, routeBuilder, page)
        { }

        public override ValidationResult IsLoaded()
        {
            throw new System.NotImplementedException();
        }
    }
}
