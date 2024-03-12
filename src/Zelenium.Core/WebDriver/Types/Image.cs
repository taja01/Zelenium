using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Zelenium.Core.WebDriver.Types
{
    public class Image : Element
    {
        public Image(ILogger<Image> logger, IWebDriver webDriver, By by) : base(logger, webDriver, by)
        {
        }

        public string AlternateText => this.Attributes.Get("alt");
        public string Source => this.Attributes.Get("src");
    }
}
