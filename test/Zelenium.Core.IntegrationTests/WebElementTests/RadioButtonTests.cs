using NUnit.Framework;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    public class RadioButtonTests : BaseTest
    {
        private WebDriver.Types.InputField Radio1 => this.mainPage.RadioButtonsSection.Radio1;
        private WebDriver.Types.InputField Radio2 => this.mainPage.RadioButtonsSection.Radio2;
        private WebDriver.Types.InputField Radio3 => this.mainPage.RadioButtonsSection.Radio3;

        [SetUp]
        public void SetUp()
        {
            this.mainPage.RadioButtonsSection.Click();
        }

        [Test]
        public void DefaultTest()
        {

            Assert.That(this.Radio1.Selected, Is.False);
            Assert.That(this.Radio2.Selected, Is.False);
            Assert.That(this.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio1Test()
        {
            this.mainPage.RadioButtonsSection.Radio1.Click();

            Assert.That(this.Radio1.Selected, Is.True);
            Assert.That(this.Radio2.Selected, Is.False);
            Assert.That(this.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio2Test()
        {
            this.Radio2.Click();

            Assert.That(this.Radio1.Selected, Is.False);
            Assert.That(this.Radio2.Selected, Is.True);
            Assert.That(this.Radio3.Selected, Is.False);
        }

        [Test]
        public void Radio3Test()
        {
            this.Radio3.Click();

            Assert.That(this.Radio1.Selected, Is.False);
            Assert.That(this.Radio2.Selected, Is.False);
            Assert.That(this.Radio3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_1()
        {
            this.Radio1.Click();
            this.Radio3.Click();

            Assert.That(this.Radio1.Selected, Is.False);
            Assert.That(this.Radio2.Selected, Is.False);
            Assert.That(this.Radio3.Selected, Is.True);
        }

        [Test]
        public void MixedTest_2()
        {
            this.Radio1.Click();
            this.Radio2.Click();
            this.Radio3.Click();
            this.Radio2.Click();

            Assert.That(this.Radio1.Selected, Is.False);
            Assert.That(this.Radio2.Selected, Is.True);
            Assert.That(this.Radio3.Selected, Is.False);
        }
    }
}
