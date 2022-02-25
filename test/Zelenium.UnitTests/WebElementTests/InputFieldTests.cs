using System.Diagnostics;
using MaterialAngular.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.UnitTests.WebElementTests
{
    [TestFixture]
    public class InputFieldTests : BaseTest
    {
        private InputFieldPage inputFieldPage;

        [SetUp]
        public void SetUp()
        {
            this.inputFieldPage = new InputFieldPage(this.driver);
            this.inputFieldPage.Load();
            Assert.IsTrue(this.inputFieldPage.IsLoaded().Passed);
        }

        [Test]
        public void ReadTest()
        {
            Assert.AreEqual("Sushi", this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteInputFieldTest()
        {
            var newText = "apple";
            this.inputFieldPage.InputField.SendKeys(newText);
            Assert.AreEqual(newText, this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteTextAreaTest()
        {
            var newText = "apple\nbananna";
            this.inputFieldPage.TextAreaField.SendKeys(newText);
        }

        [Test]
        public void ClearTest()
        {
            var newText = "apple";

            this.inputFieldPage.InputField.SendKeys(newText);
            this.inputFieldPage.InputField.Clear();

            Assert.AreEqual(string.Empty, this.inputFieldPage.InputField.Text);
        }

        [Test]
        public void WriteInputFieldExceptionTest()
        {
            var newText = "apple\napple";
            Assert.Throws<WebDriverTimeoutException>(() => this.inputFieldPage.InputField.SendKeys(newText));
        }

        [Test]
        public void HasAnyTextTest()
        {
            Assert.True(this.inputFieldPage.InputField.HasAnyText());
        }

        [Test]
        public void HasAnyTextFalseTest()
        {
            this.inputFieldPage.InputField.Clear();
            Assert.False(this.inputFieldPage.InputField.HasAnyText());
        }

        [Test]
        public void HasAnyTextTimeoutTest()
        {
            this.inputFieldPage.InputField.Clear();
            var sw = new Stopwatch();
            sw.Start();
            Assert.False(this.inputFieldPage.InputField.HasAnyText());

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(0, 2));
        }

        [Test]
        public void HasAnyTextWithTimeTest()
        {
            this.inputFieldPage.InputField.Clear();
            var sw = new Stopwatch();
            sw.Start();
            Assert.False(this.inputFieldPage.InputField.HasAnyText(TimeConfig.DefaultTimeout));

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(TimeConfig.DefaultTimeout.TotalSeconds, TimeConfig.DefaultTimeout.TotalSeconds + 2));
        }
    }
}
