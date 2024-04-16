using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class ButtonPage : AbstractLoadableContainer
    {
        public ButtonPage(ILogger logger, IWebDriver webDriver) : base(logger, webDriver, By.CssSelector(".docs-app-background"), "https://material.angular.io/components/button/overview")
        {
            this.ButtonOverview = this.Find<ButtonOverview>(By.CssSelector("button-overview-example section:nth-of-type(1)"));
            this.Header = this.Find<Header>(By.CssSelector(".docs-navbar-header"));
        }

        public Header Header { get; private set; }

        public ButtonOverview ButtonOverview { get; private set; }

        public override ValidationResult IsLoaded()
        {
            (Func<bool> Condition, string Description)[] checks =
        [
            (() => this.Displayed, "Container not loaded"),
            (() => this.ButtonOverview.Displayed, "ButtonOverview not loaded"),
            (() => this.Header.Displayed, "Header not loaded"),
        ];

            return base.CheckAllLoaded(checks);
        }
    }

    public class ButtonOverview : AbstractContainer
    {
        public ButtonOverview(ILogger logger, IWebDriver webDriver, By locator) : base(logger, webDriver, locator)
        {
            this.BasicWithoutDelay = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(1)"), System.TimeSpan.Zero);
            this.Basic = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(1)"));
            this.Primary = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(2)"));
            this.Accentc = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(3)"));
            this.Warn = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(4)"));
            this.Disabled = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(5)"));
            this.Link = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(6)"));
            this.NotExist = this.Find<Element>(By.CssSelector(".mdc-button:nth-child(111)"));
        }

        public Element Basic { get; private set; }
        public Element BasicWithoutDelay { get; private set; }
        public Element Primary { get; private set; }
        public Element Accentc { get; private set; }
        public Element Warn { get; private set; }
        public Element Disabled { get; private set; }
        public Element Link { get; private set; }
        public Element NotExist { get; private set; }


        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Container not loaded: Path{this.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
