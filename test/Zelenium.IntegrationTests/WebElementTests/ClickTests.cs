using MaterialAngular.PageObjects;
using NUnit.Framework;
using Zelenium.Core.WebDriver;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ClickTests : BaseTest
    {
        private ButtonPage buttonPage;

        [SetUp]
        public void SetUp()
        {
            this.buttonPage = new ButtonPage(this.driver);
            this.buttonPage.Load();
            Assert.That(this.buttonPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ClickTest()
        {
            this.buttonPage.Header.CdkButton.Click();

            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }

        [Test]
        public void JsClickTest()
        {
            this.buttonPage.Header.CdkButton.Click(Zelenium.Core.Enums.ClickMethod.JavaScript);

            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }

        [Test]
        public void OpenNewTabTest()
        {
            this.buttonPage.Header.CdkButton.Click(Zelenium.Core.Enums.ClickMethod.NewTab);

            var windows = this.driver.WindowHandles;
            Assert.That(windows.Count, Is.EqualTo(2));

            this.driver.SwitchTo().Window(windows[1]);
            Wait.Initialize()
                .Message("Url does not contains 'cdk'")
                .Until(() => this.driver.Url == "https://material.angular.io/cdk/categories");
        }
    }
}
