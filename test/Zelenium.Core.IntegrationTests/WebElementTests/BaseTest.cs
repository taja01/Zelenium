using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TestPage.Pages;
using Zelenium.WebDriverManager;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver driver;
        protected ILoggerFactory loggerFactory;
        protected MainPage mainPage;
        protected ILogger<BaseTest> logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                  .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                  .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                  .CreateLogger();

            this.loggerFactory = new LoggerFactory().AddSerilog();

            this.logger = this.loggerFactory.CreateLogger<BaseTest>();
        }

        [SetUp]
        public void TestSetUp()
        {
            this.driver = WebDriverFactory.GetWebDriver(Browser.Chrome, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();

            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();

            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);

            // clear logs.
            this.driver.Manage().Logs.GetLog(LogType.Browser);
        }


        [TearDown]
        public virtual void TearDown()
        {
            if (this.driver != null)
            {
                this.driver.Quit();
                this.driver.Dispose();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.CloseAndFlush();
        }
    }
}
