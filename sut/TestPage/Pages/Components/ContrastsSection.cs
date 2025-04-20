using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class ContrastsSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public Element TextWithGoodContrast => this.Find<Element>(By.CssSelector(".good-contrast-text"));
        public Element TextWithBadContrast => this.Find<Element>(By.CssSelector(".bad-contrast-text"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.TextWithGoodContrast.Displayed, $"{nameof(this.TextWithGoodContrast)} not loaded"),
                (() => this.TextWithBadContrast.Displayed, $"{nameof(this.TextWithBadContrast)} not loaded"),
                ];

            return base.IsLoaded().Merge(base.CheckAllLoaded(checks));
        }
    }
}
