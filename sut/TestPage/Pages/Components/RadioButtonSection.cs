using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class RadioButtonSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public InputField Radio1 => this.Find<InputField>(By.Id("radio1"));
        public InputField Radio2 => this.Find<InputField>(By.Id("radio2"));
        public InputField Radio3 => this.Find<InputField>(By.Id("radio3"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.Radio1.Displayed, $"{nameof(this.Radio1)} not loaded"),
                (() => this.Radio1.Displayed, $"{nameof(this.Radio2)} not loaded"),
                (() => this.Radio3.Displayed, $"{nameof(this.Radio3)} not loaded")
                ];

            return base.IsLoaded().Merge(base.CheckAllLoaded(checks));
        }
    }
}
