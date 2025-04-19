using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using TestPage.Pages;
using SelectElement = Zelenium.Core.WebDriver.Types.SelectElement;

namespace Zelenium.Core.IntegrationTests.WebElementTests
{
    public class ComboboxTests : BaseTest
    {
        private MainPage mainPage;
        private ILogger<ComboboxTests> logger;

        private SelectElement LangageSelector => this.mainPage.ComboboxSection.LanguageSelector;

        private static readonly Dictionary<string, string> ComboBoxOptions = new()
        {
            { string.Empty, "Please choose an option" },
            { "option1", "English" },
            { "option2", "German" },
            { "option3", "Hungarian" }
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var loggerFactory = new LoggerFactory().AddSerilog();
            this.logger = loggerFactory.CreateLogger<ComboboxTests>();
        }

        [SetUp]
        public void SetUp()
        {
            this.mainPage = new MainPage(this.logger, this.driver);
            this.mainPage.Load();
            Assert.That(this.mainPage.IsLoaded().Passed, Is.True);

            this.mainPage.ComboboxSection.Click();
        }

        [Test]
        public void DefaultTest()
        {
            Assert.That(this.LangageSelector.SelectedValue, Is.EqualTo(string.Empty));
            Assert.That(this.LangageSelector.SelectedText, Is.EqualTo("Please choose an option"));
        }

        [Test]
        public void OptionsTest()
        {
            var expected = new Dictionary<string, string>
            {
                { string.Empty, "Please choose an option" },
                { "option1", "English" },
                { "option2", "German" },
                { "option3", "Hungarian" }
            };

            var options = this.LangageSelector.GetAllOptions();
            Assert.That(options, Is.EquivalentTo(expected));
        }

        [Test]
        public void SetByValueTest()
        {
            foreach (var keyValuePair in ComboBoxOptions.Reverse())
            {
                this.LangageSelector.SetByValue(keyValuePair.Key);

                Assert.That(this.LangageSelector.SelectedValue, Is.EqualTo(keyValuePair.Key));
            }
        }

        [Test]
        public void SetByTextTest()
        {
            foreach (var keyValuePair in ComboBoxOptions.Reverse())
            {
                this.LangageSelector.SetByText(keyValuePair.Value);

                Assert.That(this.LangageSelector.SelectedText, Is.EqualTo(keyValuePair.Value));
            }
        }

        [Test]
        public void SetByIndexTest()
        {
            for (int i = 1; i <= ComboBoxOptions.Count; i++)
            {
                this.LangageSelector.SetByIndex(ComboBoxOptions.Count - i);

                var keyValuePair = ComboBoxOptions.ElementAt(ComboBoxOptions.Count - i);

                Assert.That(this.LangageSelector.SelectedText, Is.EqualTo(keyValuePair.Value));
                Assert.That(this.LangageSelector.SelectedValue, Is.EqualTo(keyValuePair.Key));
            }

        }
    }
}
