using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class SelectPage : AbstractLoadableContainer
    {
        public SelectPage(ILogger logger, IWebDriver webDriver) : base(logger, webDriver, By.CssSelector(".docs-component-sidenav-content"), "https://material.angular.io/components/select/overview")
        {
        }

        public SelectElement NativeSelect => Find<SelectElement>(By.CssSelector("#select-overview select"));

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
