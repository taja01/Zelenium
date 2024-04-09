using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.WebDriver;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ClickTests : BaseTest
    {
        private ButtonPage buttonPage;
        private ILogger<ClickTests> logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<ClickTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.logger, this.driver);
            this.buttonPage.Load();
            Assert.That(this.buttonPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ClickTest()
        {
            this.buttonPage.Header.CdkButton.Click();

            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }

        [Test]
        public void JsClickTest()
        {
            this.buttonPage.Header.CdkButton.Click(Enums.ClickMethod.JavaScript);

            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }

        [Test]
        public void OpenNewTabTest()
        {
            this.buttonPage.Header.CdkButton.Click(Enums.ClickMethod.NewTab);

            var windows = this.driver.WindowHandles;
            Assert.That(windows.Count, Is.EqualTo(2));

            this.driver.SwitchTo().Window(windows[1]);
            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }
    }
}
