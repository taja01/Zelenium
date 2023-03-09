using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Utils;
using Zelenium.WebDriverManager;

namespace Zelenium.IntegrationTests.WebdriverManagerTests
{
    [TestFixture]
    public class BrowserTests
    {
        private ButtonPage buttonPage;
        private IWebDriver driver;

        [TestCase(Browser.Firefox)]
        [TestCase(Browser.Chrome)]
        public void FirefoxTest(Browser browser)
        {
            this.driver = new WebDriverFactory().GetWebDriver(browser, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();
            this.buttonPage = new ButtonPage(this.driver);
            this.buttonPage.Load();
            Assertion.IsTrue(this.buttonPage.IsLoaded(), "Load test page");
        }

        [TearDown]
        public void AfterStep()
        {
            this.driver?.Dispose();
        }
    }
}
