using System.Drawing;
using NUnit.Framework;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.Utils
{
    public class ElementValidatorBuilder(IElement element, string message)
    {
        private readonly IElement element = element;
        private readonly string message = message;

        public ElementValidatorBuilder IsDisplayed()
        {
            Assertion.IsDisplayed(this.element, this.message);
            return this;
        }

        public ElementValidatorBuilder IsSelected()
        {
            Assert.That(this.element.WebElement.Selected, Is.True, $"{this.message} | Element is not Selected");
            return this;
        }

        public ElementValidatorBuilder IsNotSelected()
        {
            Assert.That(this.element.WebElement.Selected, Is.False, $"{this.message} | Element is  Selected");
            return this;
        }

        public ElementValidatorBuilder IsTextValid()
        {
            Assertion.IsTextValid(this.element, this.message);
            return this;
        }

        public ElementValidatorBuilder HasReadableContrast(double readabilityLevel = ContrastConfig.NORMAL_AAA)
        {
            Assertion.IsReadable(this.element, this.message, readabilityLevel);
            return this;
        }

        public ElementValidatorBuilder HasTextColor(Color color)
        {
            Assert.That(this.element.Color, Is.EqualTo(color), $"{this.message} | Element color is incorrect for " + this.element.Path);
            return this;
        }

        public ElementValidatorBuilder TextContains(string expectedText, bool caseSensitive = true)
        {

            var extendedMessage = $"{this.message} | Element's text does not contain the expected text";
            var text = this.element.Text;

            if (caseSensitive)
            {
                Assert.That(text, Does.Contain(expectedText), extendedMessage);
            }
            else
            {
                Assert.That(text.ContainsIgnoreCase(expectedText), Is.True, $"{extendedMessage}| Element text check. Actual '{text}' text did not contain "
                                                                        + $"the expected '{expectedText}' text (IgnoreCaseSensitie)");
            }

            return this;
        }

        public ElementValidatorBuilder TextDoesNotContains(string expectedText, bool caseSensitive = true)
        {
            var extendedMessage = $"{this.message} | Element's text does contain the expected text";
            var text = this.element.Text;

            if (caseSensitive)
            {
                Assert.That(text, Is.Not.Contains(expectedText), extendedMessage);
            }
            else
            {
                Assert.That(text.ContainsIgnoreCase(expectedText), Is.False, $"{extendedMessage}| Element text check. Actual '{text}' text did contain "
                                                                        + $"the expected that does NOT contain: '{expectedText}' text (IgnoreCaseSensitie)");
            }

            return this;
        }

        public ElementValidatorBuilder TextEqualsTo(string expectedText)
        {
            Assert.That(this.element.Text.Trim().Normalize(), Is.EqualTo(expectedText.Trim().Normalize()), this.message);
            return this;
        }

        public ElementValidatorBuilder IsTextUpperCase()
        {
            Assert.That(this.element.Text.HasLowerLetter(), Is.False);
            return this;
        }
    }
}
