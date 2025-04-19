using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages.Components
{
    public class ButtonSection(ILogger logger, IWebDriver webDriver, By locator) : BaseSectionContainer(logger, webDriver, locator)
    {
        public Element ShowAlertButton => this.Find<Element>(By.Id("alertButton"));
        public Element ExtendListButton => this.Find<Element>(By.Id("extendListItemButton"));
        public Element RemoveListItemButton => this.Find<Element>(By.Id("removeListItemButton"));
        public Element GenerateErrorButton => this.Find<Element>(By.Id("generateErrorButton"));
        public ElementList<Element> DynamicList => this.Finds<Element>(By.CssSelector("#dynamicList >li"));
        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
                [
                (() => this.Displayed, "Container not loaded"),
                (() => this.ShowAlertButton.Displayed, "ShowAlertButton not loaded"),
                (() => this.ExtendListButton.Displayed, "ExtendListButton not loaded"),
                (() => this.RemoveListItemButton.Displayed, "RemoveListItemButton not loaded"),
                (() => this.GenerateErrorButton.Displayed, "GenerateErrorButton not loaded"),
        ];

            return base.CheckAllLoaded(checks);
        }
    }
}
