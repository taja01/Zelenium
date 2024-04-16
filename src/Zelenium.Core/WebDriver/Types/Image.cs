using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Zelenium.Core.WebDriver.Types
{
    public class Image(ILogger logger, IWebDriver webDriver, By by) : Element(logger, webDriver, by)
    {
        public string AlternateText => this.Attributes.Get("alt");
        public string Source => this.Attributes.Get("src");
    }
}
