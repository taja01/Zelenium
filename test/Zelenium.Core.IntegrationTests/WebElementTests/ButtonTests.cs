using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class ButtonTests : BaseTest
    {
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

        [Test]
        public void ButtonConsoleError_Test()
        {
            var logs = this.driver.Manage().Logs.GetLog(LogType.Browser);
            Assert.That(logs, Has.Count.EqualTo(0));

            this.mainPage.ButtonSection.GenerateErrorButton.Click();


            logs = this.driver.Manage().Logs.GetLog(LogType.Browser);
            Assert.That(logs, Has.Count.EqualTo(1));
            Assert.That(logs[0].Message, Does.Contain("Generated error: Are you happy?"));

            this.logger.LogInformation("Console logs: {@logs}", logs);
        }
    }
}
