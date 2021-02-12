using System;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class ElemetWaitUntilTests : BaseTest
    {
        SnackBarPage snackBarPage;

        [SetUp]
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

        [Test]
        [Order(5)]
        public void WaitForTextTest()
        {
            this.snackBarPage.Header.CdkButton.WaitForText("cdk", caseSensitive: false);
        }

        [Test]
        [Order(6)]
        public void WaitForTextNotPresentTest()
        {
            Assert.That(() => this.snackBarPage.Header.CdkButton.WaitForText("cdk", caseSensitive: true),
                Throws.InstanceOf<WebDriverTimeoutException>());
        }

        [Test]
        [Order(7)]
        public void WaitForHasWithinTextTest()
        {
            Assert.IsFalse(this.snackBarPage.Header.CdkButton.HasTextWithin("cdk", caseSensitive: true));
        }

    }
}
