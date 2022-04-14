using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Utils;
using Zelenium.Shared;
using Zelenium.WebDriverManager;

namespace Zelenium.UnitTests.WebdriverManagerTests
{
    [TestFixture]
    public class BrowserTests
    {
        private SelectPage selectPage;
        private IWebDriver driver;
        [Test]
        public void ChromeTest()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Chrome, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assertion.IsTrue(this.selectPage.IsLoaded(), "Load test page");
        }

        [Test]
        public void FirefoxTest()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Firefox, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assertion.IsTrue(this.selectPage.IsLoaded(), "Load test page");
        }

        [TearDown]
        public void AfterStep()
        {
            this.driver?.Dispose();
        }
    }
}
