using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Zelenium.Core.WebDriver.Types
{
    /// <summary>
    /// Use for: Input and TextArea html elements
    /// </summary>
    public class InputField(ILogger logger, IWebDriver webDriver, By locator = null) : Element(logger, webDriver, locator)
    {

        /// <summary>
        /// Return input field 'Value' property
        /// </summary>
        public override string Text => this.Attributes.Get("value");

        /// <summary>
        /// Return input field 'Placeholder' property
        /// </summary>
        public string Placeholder => this.Attributes.Get("placeholder");

        /// <summary>
        /// Clear input field.
        /// Element should be Displayed!
        /// <exception cref="WebDriverTimeoutException">Throw when cannot set the text</exception>
        /// <exception cref="ElementNotVisibleException">Throw when element not visible</exception>
        /// </summary>
        public virtual void Clear()
        {
            this.Finder.GetDisplayedWebElement().Clear();
            //Wait.Initialize()
            //    .Timeout(TimeConfig.LongTimeout)
            //    .Message($"Couldn't clear text for input element {Path}")
            //    .IgnoreExceptionTypes(typeof(StaleElementReferenceException))
            //    .Until(() =>
            //    {
            //        Finder.WebElement().SendKeys(Keys.Control + "a");
            //        Finder.WebElement().SendKeys(Keys.Delete);
            //        return Value.Length == 0;

            //    });
        }

        /// <summary>
        /// Send text: clear the input field, fill it, then check it
        /// Input/TextArea have to be visible!
        /// </summary>
        /// <exception cref="WebDriverTimeoutException">Throw when cannot set the text</exception>
        /// <param name="text">Text to be send</param>
        /// <param name="timeout">Timeout, by default 5s.</param>
        public virtual void SendKeys(string text, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout)
                .Message($"Couldn't set text \"{text}\" for input element {this.Path}")
                .IgnoreExceptionTypes(typeof(StaleElementReferenceException))
                .Until(() =>
                {
                    this.Clear();
                    this.Finder.GetDisplayedWebElement().SendKeys(text);
                    return String.Compare(this.Normalize(this.Text), this.Normalize(text), StringComparison.OrdinalIgnoreCase) == 0;

                });
        }

        /// <summary>
        /// Send text without clear and check.
        /// Use it for file path.
        /// </summary>
        /// <exception cref="WebDriverTimeoutException">Throw when cannot set the text</exception>
        /// <param name="text"></param>
        public virtual void SendKeysSpecial(string text)
        {
            Do(() => this.Finder.GetWebElement().SendKeys(text));
        }

        private string Normalize(string s)
        {
            return Regex.Replace(s, @"\s+", " ", RegexOptions.Multiline).Trim();
        }
    }
}
