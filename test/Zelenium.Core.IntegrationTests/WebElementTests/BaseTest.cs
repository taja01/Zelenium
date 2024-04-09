using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.WebDriverManager;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver driver;
        protected ILoggerFactory loggerFactory;

        [SetUp]
        public void OneTimeBaseSetup()
        {
            this.driver = WebDriverFactory.GetWebDriver(Browser.Chrome, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();
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

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                          .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                          .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                          .CreateLogger();

            this.loggerFactory = new LoggerFactory().AddSerilog();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.CloseAndFlush();
        }
    }
}
