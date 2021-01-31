using OpenQA.Selenium;
using ZeleniumFramework.Model;
using ZeleniumFramework.WebDriver;

namespace MaterialAngular.PageObjects
{
    public class ButtonPage : AbstractLoadableContainer
    {
        public ButtonPage(IWebDriver webDriver) : base(webDriver, By.CssSelector(".docs-app-background"), "https://material.angular.io/components/button/overview")
        {
            this.ButtonOverview = this.Find<ButtonOverview>(By.CssSelector("button-overview-example section:nth-of-type(1)"));
            this.Header = this.Find<Header>(By.CssSelector(".docs-navbar-header"));
        }

        public Header Header { get; private set; }

        public ButtonOverview ButtonOverview { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page not loaded Path:{this.Path}" };
            }
            if (!this.ButtonOverview.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"ButtonOverview not loaded Path:{this.ButtonOverview.Path}" };
            }
            if (!this.Header.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Header not loaded: Path{this.Header.Path}" };
            }
            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }

    public class ButtonOverview : AbstractContainer
    {
        public ButtonOverview(IWebDriver webDriver, By locator) : base(webDriver, locator)
        {
            this.Basic = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(1) > span:first-of-type"));
            this.Primary = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(2) > span:first-of-type"));
            this.Accentc = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(3) > span:first-of-type"));
            this.Warn = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(4) > span:first-of-type"));
            this.Disabled = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(5) > span:first-of-type"));
            this.Link = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(6) > span:first-of-type"));
            this.NotExist = this.Find<Element>(By.CssSelector(".mat-button.mat-button-base:nth-child(16) > span:first-of-type"));
        }

        public Element Basic { get; private set; }
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
