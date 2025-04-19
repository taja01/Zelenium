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
