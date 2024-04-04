using System.Diagnostics;
using MaterialAngular.PageObjects;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Zelenium.Core.Config;

namespace Zelenium.IntegrationTests.WebElementTests
{
    [TestFixture]
    public class InputFieldTests : BaseTest
    {
        private InputFieldPage inputFieldPage;
        private ILogger<InputFieldTests> logger;

        [OneTimeSetUp]
        public void TestSetup()
        {
            this.loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = this.loggerFactory.CreateLogger<InputFieldTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.inputFieldPage = new InputFieldPage(this.logger, this.driver);
            this.inputFieldPage.Load();
            Assert.That(this.inputFieldPage.IsLoaded().Passed, Is.True);
        }

        [Test]
        public void ReadTest()
        {
            Assert.That(this.inputFieldPage.InputField.Text, Is.EqualTo("Sushi"));
        }

        [Test]
        public void WriteInputFieldTest()
        {
            var newText = "apple";
            this.inputFieldPage.InputField.SendKeys(newText);
            Assert.That(this.inputFieldPage.InputField.Text, Is.EqualTo(newText));
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

            Assert.That(this.inputFieldPage.InputField.Text, Is.EqualTo(string.Empty));
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
            Assert.That(this.inputFieldPage.InputField.HasAnyText(), Is.True);
        }

        [Test]
        public void HasAnyTextFalseTest()
        {
            this.inputFieldPage.InputField.Clear();
            Assert.That(this.inputFieldPage.InputField.HasAnyText(), Is.False);
        }

        [Test]
        public void HasAnyTextTimeoutTest()
        {
            this.inputFieldPage.InputField.Clear();
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.inputFieldPage.InputField.HasAnyText(), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(0, 2));
        }

        [Test]
        public void HasAnyTextWithTimeTest()
        {
            this.inputFieldPage.InputField.Clear();
            var sw = new Stopwatch();
            sw.Start();
            Assert.That(this.inputFieldPage.InputField.HasAnyText(TimeConfig.DefaultTimeout), Is.False);

            sw.Stop();

            Assert.That(sw.Elapsed.TotalSeconds, Is.InRange(TimeConfig.DefaultTimeout.TotalSeconds, TimeConfig.DefaultTimeout.TotalSeconds + 2));
        }
    }
}
