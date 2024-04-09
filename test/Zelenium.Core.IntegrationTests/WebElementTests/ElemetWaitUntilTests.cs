using System;
using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.WebDriverManager;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ElemetWaitUntilTests
    {
        SnackBarPage snackBarPage;
        IWebDriver driver;
        ILogger<ElemetWaitUntilTests> logger;

        [OneTimeSetUp]
        public void SetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<ElemetWaitUntilTests>();

            this.driver = WebDriverFactory.GetWebDriver(Browser.Chrome, runInHeadlessMode: true, useModHeader: false);
            this.driver.Manage().Window.Maximize();

            this.snackBarPage = new SnackBarPage(this.logger, this.driver);
            this.snackBarPage.Load();
            Assert.That(this.snackBarPage.IsLoaded().Passed, Is.True);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.driver.Quit();
            this.driver.Dispose();
            Log.CloseAndFlush();
        }

        [Test]
        [Order(1)]
        public void PresentNowNegativeTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.That(this.snackBarPage.SnackBar.PresentNow, Is.False);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(1000));
        }

        [Test]
        [Order(2)]
        public void DisplayNowNegativeTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.That(this.snackBarPage.SnackBar.DisplayedNow, Is.False);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(1000));
        }

        [Test]
        [Order(3)]
        public void WaitForTheSnackBarNegativeTest()
        {
            var exception = Assert.Throws<WebDriverTimeoutException>(() => this.snackBarPage.SnackBar.WaitUntilDisplay("snackbar", TimeSpan.FromSeconds(1)));
            Assert.That(exception.Message, Does.Contain("snackbar"));
        }

        [Test]
        [Order(4)]
        public void WaitForDisappearTheSnackBarNegativeTest()
        {
            this.snackBarPage.SnackBar.WaitUntilDisappear("snackbar");
        }

        [Test]
        [Order(5)]
        public void WaitForTheSnackBarTest()
        {
            this.snackBarPage.ShowSnackBarButton.Click();
            this.snackBarPage.SnackBar.WaitUntilDisplay("snackbar");
        }

        [Test]
        [Order(6)]
        public void PresentNowTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.That(this.snackBarPage.SnackBar.PresentNow, Is.True);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(1000));
        }

        [Test]
        [Order(7)]
        public void DisplayNowTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.That(this.snackBarPage.SnackBar.DisplayedNow, Is.True);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(1000));
        }

        [Test]
        [Order(8)]
        public void WaitForDisappearTheSnackBarTest()
        {
            this.snackBarPage.SnackBar.CloseButton.Click();
            this.snackBarPage.SnackBar.WaitUntilDisappear("snackbar");
        }

        [Test]
        [Order(9)]
        public void WaitForTextTest()
        {
            this.snackBarPage.Header.CdkButton.WaitForText("cdk", caseSensitive: false);
        }

        [Test]
        [Order(10)]
        public void WaitForTextNotPresentTest()
        {
            Assert.That(() => this.snackBarPage.Header.CdkButton.WaitForText("cdk", caseSensitive: true),
                Throws.InstanceOf<WebDriverTimeoutException>());
        }

        [Test]
        [Order(11)]
        public void WaitForHasWithinTextTest()
        {
            Assert.That(this.snackBarPage.Header.CdkButton.HasTextWithin("cdk", caseSensitive: true), Is.False);
        }

    }
}
