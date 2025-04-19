using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class ComboboxSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public SelectElement LanguageSelector => this.Find<SelectElement>(By.Id("languageSelector"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, $"{nameof(ComboboxSection)} not loaded"),
                (() => this.LanguageSelector.Displayed, $"{nameof(this.LanguageSelector)} not loaded")
                ];

            return base.IsLoaded().Merge(base.CheckAllLoaded(checks));
        }
    }
}
