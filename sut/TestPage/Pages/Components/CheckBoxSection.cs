using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class CheckBoxSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public InputField Option1 => this.Find<InputField>(By.Id("checkbox1"));
        public InputField Option2 => this.Find<InputField>(By.Id("checkbox2"));
        public InputField Option3 => this.Find<InputField>(By.Id("checkbox3"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.Option1.Displayed, "Option1 not loaded"),
                (() => this.Option2.Displayed , "Option2 not loaded"),
                (() => this.Option3.Displayed, "Option3 not loaded"),
        ];

            return base.IsLoaded().Merge(base.CheckAllLoaded(checks));
        }
    }
}
