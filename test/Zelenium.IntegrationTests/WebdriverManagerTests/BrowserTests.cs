﻿using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.Utils;
using Zelenium.WebDriverManager;

namespace Zelenium.IntegrationTests.WebdriverManagerTests
{
    [TestFixture]
    public class BrowserTests
    {
        private ButtonPage buttonPage;
        private IWebDriver driver;
        private ILogger<ButtonPage> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            logger = loggerFactory.CreateLogger<ButtonPage>();
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
            logger.LogInformation("Start Test");
            logger.LogInformation("BrowserTest: {browser}", browser);

            this.driver = new WebDriverFactory().GetWebDriver(browser, runInHeadlessMode: false, useModHeader: false);
            this.driver.Manage().Window.Maximize();
            this.buttonPage = new ButtonPage(logger, this.driver);
            this.buttonPage.Load();
            Assertion.IsTrue(this.buttonPage.IsLoaded(), "Load test page");
        }

        [TearDown]
        public void AfterTest()
        {
            logger.LogInformation("End Test");

            this.driver?.Quit();
            this.driver?.Dispose();
        }
    }
}
