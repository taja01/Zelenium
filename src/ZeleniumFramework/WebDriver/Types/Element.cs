using System;
using System.Drawing;
using OpenQA.Selenium;
using ZeleniumFramework.WebDriver.Interfaces;

namespace ZeleniumFramework.WebDriver
{
    public class Element : AbstractElement, IElement
    {
        public Element(IWebDriver webDriver, By by = null) : base(webDriver, by)
        {
        }

        public string Text => this.Finder.WebElement().Text;
        public Color Color => this.GetColor();

        public void WaitForText(string expectedText, bool caseSensitive = true, TimeSpan? timeout = null, string errorMessage = null)
        {
            var messagePrefix = errorMessage == null ? string.Empty : "[" + errorMessage + "] ";

            Wait.Initialize()
            .Timeout(timeout)
            .Until(() =>
            {
                var actualText = this.Text;
                return caseSensitive
                             ? expectedText.Equals(actualText)
                             : expectedText.ToLower().Normalize() == actualText.ToLower().Normalize();
            });
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
    }
}
