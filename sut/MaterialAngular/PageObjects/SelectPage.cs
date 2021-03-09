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

        public Element IFrame => this.Find<Element>(By.CssSelector("div:nth-child(2) > div:nth-child(3) > iframe"));
        public ShadowContainer Container => new ShadowContainer(this.webDriver, By.CssSelector("#output > shadow-output"));

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }
            if (!this.IFrame.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"IFrame element not loaded \n{this.IFrame.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
