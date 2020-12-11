using System;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ZeleniumFrameworkTest.WebElementTests
{
    [TestFixture]
    public class ElemetWaitUntilTests : TestBase
    {
        SnackBarPage snackBarPage;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.snackBarPage = new SnackBarPage(this.driver);
            this.snackBarPage.Load();
            Assert.IsTrue(this.snackBarPage.IsLoaded().Passed);
        }       

        [Test]
        [Order(1)]
        public void WaitForTheSnackBarNegativeTest()
        {
            var exception = Assert.Throws<WebDriverTimeoutException>(() => this.snackBarPage.SnackBar.WaitUntilDisplay("snackbar", TimeSpan.FromSeconds(1)));
            StringAssert.Contains("snackbar", exception.Message);
        }

        [Test]
        [Order(2)]
        public void WaitForDisappearTheSnackBarNegativeTest()
        {
            this.snackBarPage.SnackBar.WaitUntilDisappear("snackbar");
        }

        [Test]
        [Order(3)]
        public void WaitForTheSnackBarTest()
        {
            this.snackBarPage.ShowSnackBarButton.Click();
            this.snackBarPage.SnackBar.WaitUntilDisplay("snackbar");
        }

        [Test]
        [Order(4)]
        public void WaitForDisappearTheSnackBarTest()
        {
            this.snackBarPage.SnackBar.WaitUntilDisappear("snackbar");
        }
    }
}
