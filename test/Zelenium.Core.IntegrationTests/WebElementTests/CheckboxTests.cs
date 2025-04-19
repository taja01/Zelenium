using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using TestPage.Pages;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class CheckboxTests : BaseTest
    {
        private MainPage mainPage;
        private ILogger<CheckboxTests> logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<CheckboxTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);

            this.mainPage.CheckBoxSection.Click();
        }

        [Test]
        public void DefaultTest()
        {
            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.False);
        }

        [Test]
        public void Option1Test()
        {
            this.mainPage.CheckBoxSection.Option1.Click();

            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.True);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.False);
        }

        [Test]
        public void Option2Test()
        {
            this.mainPage.CheckBoxSection.Option2.Click();

            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.True);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.False);
        }

        [Test]
        public void Option3Test()
        {
            this.mainPage.CheckBoxSection.Option3.Click();

            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_1()
        {
            this.mainPage.CheckBoxSection.Option1.Click();
            this.mainPage.CheckBoxSection.Option3.Click();

            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.True);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.False);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_2()
        {
            this.mainPage.CheckBoxSection.Option1.Click();
            this.mainPage.CheckBoxSection.Option2.Click();
            this.mainPage.CheckBoxSection.Option3.Click();

            Assert.That(this.mainPage.CheckBoxSection.Option1.Selected, Is.True);
            Assert.That(this.mainPage.CheckBoxSection.Option2.Selected, Is.True);
            Assert.That(this.mainPage.CheckBoxSection.Option3.Selected, Is.True);
        }
    }
}
