using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using TestPage.Pages;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    public class RadioButtonTests : BaseTest
    {
        private MainPage mainPage;
        private ILogger<RadioButtonTests> logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<RadioButtonTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);

            this.mainPage.RadioButtonsSection.Click();
        }

        [Test]
        public void DefaultTest()
        {
            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio1Test()
        {
            this.mainPage.RadioButtonsSection.Radio1.Click();

            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.True);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio2Test()
        {
            this.mainPage.RadioButtonsSection.Radio2.Click();

            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.True);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio3Test()
        {
            this.mainPage.RadioButtonsSection.Radio3.Click();

            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_1()
        {
            this.mainPage.RadioButtonsSection.Radio1.Click();
            this.mainPage.RadioButtonsSection.Radio3.Click();

            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_2()
        {
            this.mainPage.RadioButtonsSection.Radio1.Click();
            this.mainPage.RadioButtonsSection.Radio2.Click();
            this.mainPage.RadioButtonsSection.Radio3.Click();
            this.mainPage.RadioButtonsSection.Radio2.Click();

            Assert.That(this.mainPage.RadioButtonsSection.Radio1.Selected, Is.False);
            Assert.That(this.mainPage.RadioButtonsSection.Radio2.Selected, Is.True);
            Assert.That(this.mainPage.RadioButtonsSection.Radio3.Selected, Is.False);
        }
    }
}
