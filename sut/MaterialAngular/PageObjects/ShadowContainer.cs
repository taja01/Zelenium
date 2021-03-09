using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class ShadowContainer : AbstractContainer
    {
        public ShadowContainer(IWebDriver webDriver, By locator) : base(webDriver, locator)
        {
        }

        public SelectElement Pets => this.FindShadow<SelectElement>(By.CssSelector("#pet-select"));
        public ElementList<Element> Title => this.FindsShadow<Element>(By.CssSelector("label"));

        public override ValidationResult IsLoaded()
        {
            return new ValidationResult { Message = "", Passed = true };
        }
    }
}
