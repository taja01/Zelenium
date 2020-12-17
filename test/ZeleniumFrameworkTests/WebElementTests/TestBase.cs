using NUnit.Framework;
using OpenQA.Selenium;
using ZeleniumFramework.Driver;
using ZeleniumFramework.Enums;

namespace ZeleniumFrameworkTest.WebElementTests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeBaseSetup()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Chrome, false);
            this.driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void TearDowb()
        {
            this.driver.Quit();
            this.driver.Dispose();
        }
    }
}
