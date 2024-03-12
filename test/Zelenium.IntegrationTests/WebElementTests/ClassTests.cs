using System;
using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.Utils;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ClassTests : BaseTest
    {
        TabsPage tabsPage;
        const string ACTIVE_CLASS = "mdc-tab--active";
        const string FAKE_CLASS = "blabla";
        private ILogger<TabsPage> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            logger = loggerFactory.CreateLogger<TabsPage>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Log.CloseAndFlush();
        }

        [SetUp]
        public void SetUp()
        {
            this.driver.Url = "https://material.angular.io/components/tabs/overview";
            this.tabsPage = new TabsPage(this.logger, this.driver);
            Assertion.IsTrue(this.tabsPage.IsLoaded());
        }

        [Test]
        [Order(1)]
        public void HasClassTest()
        {
            Assert.That(this.tabsPage.Tab1.Class.Has(ACTIVE_CLASS), Is.True);
        }

        [Test]
        [Order(2)]
        public void HasClassNegativeTest()
        {
            Assert.That(this.tabsPage.Tab1.Class.Has(FAKE_CLASS), Is.False);
        }

        [Test]
        [Order(3)]
        public void HasWithinClassNegativeTest()
        {
            Assert.That(this.tabsPage.Tab1.Class.HasWithin(FAKE_CLASS, TimeSpan.FromSeconds(1)), Is.False);
        }

        [Test]
        [Order(4)]
        public void WaitForClassExceptionTest()
        {
            Assert.That(() => this.tabsPage.Tab1.Class.WaitForClass(FAKE_CLASS, TimeSpan.FromSeconds(1)),
               Throws.TypeOf<WebDriverTimeoutException>()
               .With.Message.Contains($"Element has NOT got '{FAKE_CLASS}' class"));
        }

        [Test]
        [Order(5)]
        public void WaitForRemoveClassExceptionTest()
        {
            Assert.That(() => this.tabsPage.Tab1.Class.WaitForRemoveClass(ACTIVE_CLASS, TimeSpan.FromSeconds(1)),
               Throws.TypeOf<WebDriverTimeoutException>()
               .With.Message.Contains($"Element still has got '{ACTIVE_CLASS}' class"));
        }
    }
}
