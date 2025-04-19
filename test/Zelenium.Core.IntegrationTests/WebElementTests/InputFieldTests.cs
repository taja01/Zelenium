using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class InputFieldTests : BaseTest
    {
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
        public void WriteInputFieldTest()
        {
            var newText = "apple";
            this.mainPage.InputFieldsSection.GenericField.SendKeys(newText);
            Assert.That(this.mainPage.InputFieldsSection.GenericField.Text, Is.EqualTo(newText));
        }

        [Test]
        public void WriteTextAreaTest()
        {
            var newText = "apple\nbananna";
            this.mainPage.InputFieldsSection.TextArea.Clear();
            this.mainPage.InputFieldsSection.TextArea.SendKeys(newText);
        }

        [Test]
        public void ClearTest()
        {
            var newText = "apple";

            this.mainPage.InputFieldsSection.GenericField.SendKeys(newText);
            this.mainPage.InputFieldsSection.GenericField.Clear();

            Assert.That(this.mainPage.InputFieldsSection.GenericField.Text, Is.EqualTo(string.Empty));
        }

        [Test]
        public void WriteInputFieldExceptionTest()
        {
            var newText = "apple\napple";
            Assert.Throws<WebDriverTimeoutException>(() => this.mainPage.InputFieldsSection.GenericField.SendKeys(newText));
        }

        [Test]
        public void HasAnyTextTest()
        {
            Assert.That(this.mainPage.InputFieldsSection.TextArea.HasAnyText(), Is.True);
        }

        [Test]
        public void HasAnyTextFalseTest()
        {
            this.mainPage.InputFieldsSection.GenericField.Clear();
            Assert.That(this.mainPage.InputFieldsSection.GenericField.HasAnyText(), Is.False);
        }

        [Test]
        public void HasAnyTextTimeoutTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.mainPage.InputFieldsSection.GenericField.HasAnyText(), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(0, 2));
        }

        [Test]
        public void HasAnyTextWithTimeTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.mainPage.InputFieldsSection.GenericField.HasAnyText(TimeConfig.DefaultTimeout), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(TimeConfig.DefaultTimeout.TotalSeconds, TimeConfig.DefaultTimeout.TotalSeconds + 2));
        }
    }
}
