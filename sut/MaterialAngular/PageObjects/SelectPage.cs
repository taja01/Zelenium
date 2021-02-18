using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class SelectPage : AbstractLoadableContainer
    {
        public SelectPage(IWebDriver webDriver)
            : base(webDriver, By.CssSelector(".page-wrapper"), "https://developer.mozilla.org/en-US/docs/Web/HTML/Element/select")
        {
        }

        public SelectElement Language => this.Find<SelectElement>(By.CssSelector("#select_language"));

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }
            if (!this.Language.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"FavoriteFood element not loaded \n{this.Language.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
