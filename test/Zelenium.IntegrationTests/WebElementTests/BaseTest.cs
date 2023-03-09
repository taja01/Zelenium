using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.WebDriverManager;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void OneTimeBaseSetup()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Chrome, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            if (this.driver != null)
            {
                this.driver.Quit();
                this.driver.Dispose();
            }
        }
    }
}
