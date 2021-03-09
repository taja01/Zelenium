using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class InputFieldPage : AbstractLoadableContainer
    {
        public InputFieldPage(IWebDriver webDriver) : base(webDriver, By.CssSelector(".docs-app-background"), "https://material.angular.io/components/input/overview")
        {
            this.PageThisInputField = this.Find<InputField>(By.CssSelector("#mat-input-0"));

        }

        public InputField PageThisInputField { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page not loaded Path:{this.Path}" };
            }
            if (!this.PageThisInputField.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"ReadThisInputField not loaded Path:{this.PageThisInputField.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
