using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class InputFieldsSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public InputField GenericField => this.Find<InputField>(By.Id("englishInput"));
        public Element GenericErrorLabel => this.Find<Element>(By.Id("validationError"));
        public InputField DisabledField => this.Find<InputField>(By.Id("readonlyField"));
        public InputField TextArea => this.Find<InputField>(By.Id("story"));

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.GenericField.Displayed, "Option1 not loaded"),
                (() => this.DisabledField.Displayed , "Option2 not loaded"),
                (() => this.TextArea.Displayed, "Option3 not loaded"),
        ];

            return base.IsLoaded().Merge(base.CheckAllLoaded(checks));
        }
    }
}
