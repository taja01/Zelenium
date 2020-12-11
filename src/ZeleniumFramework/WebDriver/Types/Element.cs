using System;
using System.Drawing;
using OpenQA.Selenium;

namespace ZeleniumFramework.WebDriver
{
    public class Element : AbstractElement
    {
        public Element(IWebDriver webDriver, By by = null) : base(webDriver, by)
        {
        }

        public Color Color => this.GetColor();
        public string Text => this.Finder.WebElement().Text;

        public void WaitForText(string expectedText, TimeSpan? timeout = null, string errorMessage = null)
        {
            var messagePrefix = errorMessage == null ? string.Empty : "[" + errorMessage + "] ";

            Wait.Initialize()
            .Timeout(timeout)
            .Until(() =>
            {
                var actualText = this.Text;
                return expectedText.Equals(actualText);
            });
        }

        public bool HasTextWithin(string expectedText, TimeSpan? timeout = null)
        {
            try
            {
                this.WaitForText(expectedText, timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
