using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class InputFieldPage : AbstractLoadableContainer
    {
        public InputFieldPage(ILogger logger, IWebDriver webDriver) : base(logger, webDriver, By.CssSelector(".docs-app-background"), "https://material.angular.io/components/input/overview")
        {
            this.InputField = this.Find<InputField>(By.CssSelector("#mat-input-0"));
            this.TextAreaField = this.Find<InputField>(By.CssSelector("#mat-input-1"));
        }

        public InputField InputField { get; private set; }
        public InputField TextAreaField { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page not loaded Path:{this.Path}" };
            }
            if (!this.InputField.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"InputField not loaded Path:{this.InputField.Path}" };
            }
            if (!this.TextAreaField.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"TextAreaField not loaded Path:{this.TextAreaField.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
