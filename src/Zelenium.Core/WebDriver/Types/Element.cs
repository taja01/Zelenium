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

        /// <summary>
        /// Get element's text
        /// </summary>
        public virtual string Text => this.GetText();

        /// <summary>
        /// Get element's color
        /// </summary>
        public Color Color => this.GetColor();

        /// <summary>
        /// Wait for the text
        /// </summary>
        /// <param name="expectedText">The text we are waiting</param>
        /// <param name="caseSensitive">Case sensitive flag</param>
        /// <param name="timeout">Default is 5s</param>
        /// <param name="errorMessage">Exception contains this message</param>
        /// <exception cref="WebDriverTimeoutException">Time out exception</exception>
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


        /// <summary>
        /// True of False weither the expected text present on the element
        /// </summary>
        /// <param name="expectedText">The text we are waiting for</param>
        /// <param name="caseSensitive">Case sensitive flag</param>
        /// <param name="timeout">Default is 5s</param>
        /// <returns>Boolean true, false</returns>
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

        /// <summary>
        /// True of False, weither the element has text or not
        /// </summary>
        /// <param name="timeout">Maximum time to wait. Default is 0s</param>
        /// <returns>Boolean true, false</returns>
        public bool HasAnyText(TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                  .Timeout(timeout ?? TimeSpan.Zero)
                  .Success(() =>
                  {
                      return this.Text.Length != 0;
                  });
        }

        private string GetText()
        {
            this.Scroll();
            return this.Finder.GetWebElement().Text;
        }
    }
}
