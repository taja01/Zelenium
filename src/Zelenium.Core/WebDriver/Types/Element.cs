using System;
using System.Drawing;
using OpenQA.Selenium;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver.Types
{
    public class Element : AbstractElement, IElement
    {
        public Element(IWebDriver webDriver, By by = null) : base(webDriver, by)
        {
        }

        public virtual string Text => this.GetText();
        public Color Color => this.GetColor();

        public void WaitForText(string expectedText, bool caseSensitive = true, TimeSpan? timeout = null, string errorMessage = null)
        {
            var actualText = string.Empty;
            try
            {
                Wait.Initialize()
                    .Timeout(timeout)
                    .Until(() =>
                    {
                        actualText = this.Text;
                        return caseSensitive
                            ? expectedText.Trim().Equals(actualText.Trim())
                            : expectedText.ToLower().Normalize().Trim() == actualText.ToLower().Normalize().Trim();
                    });
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"Expected text: '{expectedText}', but on element the text was: '{actualText}'.\nCase sensitive: {caseSensitive}\n{errorMessage}");
            }
        }

        public bool HasTextWithin(string expectedText, bool caseSensitive = true, TimeSpan? timeout = null)
        {
            try
            {
                this.WaitForText(expectedText, caseSensitive, timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        private string GetText()
        {
            this.Scroll();
            return this.Finder.GetWebElement().Text;
        }
    }
}
