using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
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
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Chrome, false);
            this.driver.Manage().Window.Maximize();
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assert.IsTrue(this.selectPage.IsLoaded().Passed);
        }

        [Test]
        public void FirefoxTest()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Firefox, false);
            this.driver.Manage().Window.Maximize();
            this.selectPage = new SelectPage(this.driver);
            this.selectPage.Load();
            Assert.IsTrue(this.selectPage.IsLoaded().Passed);
        }

        [TearDown]
        public void AfterStep()
        {
            this.driver?.Dispose();
        }
    }
}
