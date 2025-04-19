using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class BaseSectionContainer(ILogger logger, IWebDriver webDriver, By locator) : AbstractContainer(logger, webDriver, locator)
    {
        private Element Content => this.Find<Element>(By.CssSelector(".section-content"));

        public bool IsOpen()
        {
            return this.Content.DisplayedNow;
        }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Container does not\n{this.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
