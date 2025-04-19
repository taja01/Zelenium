using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TestPage.Pages;
using Zelenium.Core.Utils;
using Zelenium.WebDriverManager;

namespace Zelenium.Core.IntegrationTests.WebdriverManagerTests
{
    [TestFixture]
    public class BrowserTests
    {
        private MainPage mainPage;
        private IWebDriver driver;
        private ILogger<MainPage> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<MainPage>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Log.CloseAndFlush();
        }

        [TestCase(Browser.Firefox)]
        [TestCase(Browser.Chrome)]
        public void BrowserTest(Browser browser)
        {
            this.logger.LogInformation("Start Test");
            this.logger.LogInformation("BrowserTest: {browser}", browser);

            this.driver = WebDriverFactory.GetWebDriver(browser, runInHeadlessMode: false, useModHeader: false);
            this.driver.Manage().Window.Maximize();
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assertion.IsTrue(this.mainPage.IsLoaded(), "Load test page");
        }

        [TearDown]
        public void AfterTest()
        {
            this.logger.LogInformation("End Test");

            this.driver?.Quit();
            this.driver?.Dispose();
        }
    }
}
