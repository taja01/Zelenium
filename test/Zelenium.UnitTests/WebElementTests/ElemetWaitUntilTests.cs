using System;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Shared;
using Zelenium.WebDriverManager;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class ElemetWaitUntilTests
    {
        SnackBarPage snackBarPage;
        IWebDriver driver;
        [OneTimeSetUp]
        public void SetUp()
        {
            this.driver = new WebDriverFactory().GetWebDriver(Browser.Chrome, debug: false);
            this.driver.Manage().Window.Maximize();

            this.snackBarPage = new SnackBarPage(this.driver);
            this.snackBarPage.Load();
            Assert.IsTrue(this.snackBarPage.IsLoaded().Passed);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.driver.Quit();
            this.driver.Dispose();
        }

        [Test]
        [Order(1)]
        public void PresentNowNegativeTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.IsFalse(this.snackBarPage.SnackBar.PresentNow);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 1000);
        }

        [Test]
        [Order(2)]
        public void DisplayNowNegativeTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.IsFalse(this.snackBarPage.SnackBar.DisplayedNow);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 1000);
        }

        [Test]
        [Order(3)]
        public void WaitForTheSnackBarNegativeTest()
        {
            var exception = Assert.Throws<WebDriverTimeoutException>(() => this.snackBarPage.SnackBar.WaitUntilDisplay("snackbar", TimeSpan.FromSeconds(1)));
            StringAssert.Contains("snackbar", exception.Message);
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
            Assert.IsTrue(this.snackBarPage.SnackBar.PresentNow);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 1000);
        }

        [Test]
        [Order(7)]
        public void DisplayNowTest()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Assert.IsTrue(this.snackBarPage.SnackBar.DisplayedNow);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.ElapsedMilliseconds);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 1000);
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
            Assert.IsFalse(this.snackBarPage.Header.CdkButton.HasTextWithin("cdk", caseSensitive: true));
        }

    }
}
