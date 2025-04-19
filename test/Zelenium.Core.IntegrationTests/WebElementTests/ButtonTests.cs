using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using TestPage.Pages;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ButtonTests : BaseTest
    {
        private MainPage mainPage;
        private ILogger<ButtonTests> logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<ButtonTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void AlertButtonTest()
        {
            this.mainPage.ButtonSection.ShowAlertButton.Click();

            IAlert alert = this.driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("This is a triggered alert from the Main Page!"));

            alert.Accept();
        }

        [Test]
        public void DynamicButtonTest_Zero()
        {
            Assert.That(this.mainPage.ButtonSection.DynamicList, Has.Count.EqualTo(0));
        }

        [Test]
        public void DynamicButtonTest_Adding()
        {
            this.mainPage.ButtonSection.ExtendListButton.Click();

            Assert.That(this.mainPage.ButtonSection.DynamicList, Has.Count.EqualTo(1));
        }

        [Test]
        public void DynamicButtonTest_Adding_Removing()
        {
            this.mainPage.ButtonSection.ExtendListButton.Click();
            this.mainPage.ButtonSection.RemoveListItemButton.Click();

            Assert.That(this.mainPage.ButtonSection.DynamicList, Has.Count.EqualTo(0));
        }
    }
}
