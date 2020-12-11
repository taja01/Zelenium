using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ZeleniumFrameworkTest.WebElementTests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeBaseSetup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            this.driver = new ChromeDriver(options);

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
