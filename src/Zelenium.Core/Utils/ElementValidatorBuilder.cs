using System.Drawing;
using NUnit.Framework;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.Utils
{
    public class ElementValidatorBuilder
    {
        private readonly IElement element;
        private readonly string message;

        public ElementValidatorBuilder(IElement element, string message)
        {
            this.element = element;
            this.message = message;
        }

        public ElementValidatorBuilder IsDisplayed()
        {
            Assertion.IsDisplayed(this.element, this.message);
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
            Assert.AreEqual(color, this.element.Color, $"{this.message} | Element color is incorrect for " + this.element.Path);
            return this;
        }

        public ElementValidatorBuilder TextContains(string expectedText, bool caseSensitive = true)
        {

            var extendedMessage = $"{this.message} | Element's text does not contain the expected text";
            var text = this.element.Text;

            if (caseSensitive)
            {
                StringAssert.Contains(expectedText, text, extendedMessage);
            }
            else
            {
                Assert.IsTrue(text.ContainsIgnoreCase(expectedText), $"{extendedMessage}| Element text check. Actual '{text}' text did not contain "
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
                StringAssert.DoesNotContain(expectedText, text, extendedMessage);
            }
            else
            {
                Assert.IsFalse(text.ContainsIgnoreCase(expectedText), $"{extendedMessage}| Element text check. Actual '{text}' text did contain "
                                                                        + $"the expected that does NOT contain: '{expectedText}' text (IgnoreCaseSensitie)");
            }

            return this;
        }

        public ElementValidatorBuilder TextEqualsTo(string expectedText)
        {
            Assert.AreEqual(expectedText.Trim().Normalize(), this.element.Text.Trim().Normalize(), this.message);
            return this;
        }

        public ElementValidatorBuilder IsTextUpperCase()
        {
            Assert.IsFalse(this.element.Text.HasLowerLetter());
            return this;
        }
    }
}
