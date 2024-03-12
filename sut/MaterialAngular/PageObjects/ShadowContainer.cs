using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class ShadowContainer : AbstractContainer
    {
        public ShadowContainer(ILogger<ShadowContainer> logger, IWebDriver webDriver, By locator)
            : base(logger, webDriver, locator)
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
