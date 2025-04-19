using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class InputFieldTests : BaseTest
    {
        private WebDriver.Types.InputField GenericInputField => this.mainPage.InputFieldsSection.GenericField;
        private WebDriver.Types.InputField TextArea => this.mainPage.InputFieldsSection.TextArea;

        [SetUp]
        public void SetUp()
        {
            this.mainPage.InputFieldsSection.Click();
        }

        [Test]
        public void ReadTest()
        {
            Assert.That(this.mainPage.InputFieldsSection.DisabledField.Text, Is.EqualTo("This is read-only"));
        }

        [Test]
        public void DisabledFieldTest()
        {
            Assert.That(this.mainPage.InputFieldsSection.DisabledField.Enabled, Is.False);
        }

        [Test]
        public void EnbledFieldTest()
        {
            Assert.That(this.GenericInputField.Enabled, Is.True);
        }

        [Test]
        public void WriteInputFieldTest()
        {
            var newText = "apple";
            this.GenericInputField.SendKeys(newText);
            Assert.That(this.GenericInputField.Text, Is.EqualTo(newText));
        }

        [Test]
        public void WriteTextAreaTest()
        {
            var newText = "apple\nbananna";
            this.TextArea.Clear();
            this.TextArea.SendKeys(newText);
        }

        [Test]
        public void ClearTest()
        {
            var newText = "apple";


            this.GenericInputField.SendKeys(newText);
            this.GenericInputField.Clear();

            Assert.That(this.GenericInputField.Text, Is.EqualTo(string.Empty));
        }

        [Test]
        public void WriteInputFieldExceptionTest()
        {
            var newText = "apple\napple";
            Assert.Throws<WebDriverTimeoutException>(() => this.GenericInputField.SendKeys(newText));
        }

        [Test]
        public void HasAnyTextTest()
        {
            Assert.That(this.TextArea.HasAnyText(), Is.True);
        }

        [Test]
        public void HasAnyTextFalseTest()
        {
            this.GenericInputField.Clear();
            Assert.That(this.GenericInputField.HasAnyText(), Is.False);
        }

        [Test]
        public void HasAnyTextTimeoutTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.GenericInputField.HasAnyText(), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(0, 2));
        }

        [Test]
        public void HasAnyTextWithTimeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.GenericInputField.HasAnyText(TimeConfig.DefaultTimeout), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(TimeConfig.DefaultTimeout.TotalSeconds, TimeConfig.DefaultTimeout.TotalSeconds + 2));
        }
    }
}
