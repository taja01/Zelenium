using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using TestPage.Pages;
using Zelenium.Core.WebDriver;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ClickTests : BaseTest
    {
        private MainPage mainPage;
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
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ClickTest()
        {
            this.mainPage.NavBar.LoginButton.Click();

            Wait.Initialize()
                .Message("Browser not redirected to Login page")
                .Until(() => this.driver.Url == "https://taja01.github.io/testpage/login.html");
        }

        [Test]
        public void JsClickTest()
        {
            this.mainPage.NavBar.LoginButton.Click(Enums.ClickMethod.JavaScript);

            Wait.Initialize()
                .Message("Browser not redirected to Login page")
                .Until(() => this.driver.Url == "https://taja01.github.io/testpage/login.html");
        }

        [Test]
        public void OpenNewTabTest()
        {
            this.mainPage.NavBar.LoginButton.Click(Enums.ClickMethod.NewTab);

            var windows = this.driver.WindowHandles;
            Assert.That(windows.Count, Is.EqualTo(2));

            this.driver.SwitchTo().Window(windows[1]);
            Wait.Initialize()
                .Message("Browser not redirected to Login page")
                .Until(() => this.driver.Url == "https://taja01.github.io/testpage/login.html");
        }
    }
}
