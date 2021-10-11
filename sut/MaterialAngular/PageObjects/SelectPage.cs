using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class SelectPage : AbstractLoadableContainer
    {
        public SelectPage(IWebDriver webDriver)
            : base(webDriver, By.CssSelector("#editor-container"), "https://interactive-examples.mdn.mozilla.net/pages/tabbed/select.html")
        {
        }

        public ShadowContainer Container => new ShadowContainer(this.webDriver, By.CssSelector("#output > shadow-output"));
        public ElementList<Element> Tabs => Finds<Element>(By.CssSelector("[role='tab']"));

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }
            if (!this.Container.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"IFrame element not loaded \n{this.Container.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
