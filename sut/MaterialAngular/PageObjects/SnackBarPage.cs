using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class SnackBarPage : AbstractLoadableContainer
    {
        public SnackBarPage(IWebDriver webDriver) : base(webDriver, null, "https://material.angular.io/components/snack-bar/overview")
        {
            this.ShowSnackBarButton = this.Find<Element>(By.CssSelector("snack-bar-overview-example > button"));
            this.SnackBar = this.Find<SnackBarContent>(By.CssSelector(".mdc-snackbar"));
            this.Header = this.Find<Header>(By.CssSelector(".docs-navbar-header"));
        }

        public Header Header { get; private set; }
        public Element ShowSnackBarButton { get; private set; }
        public SnackBarContent SnackBar { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.ShowSnackBarButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"ShowSnackBar not displayed\n Path: {this.ShowSnackBarButton.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }


        public class SnackBarContent : AbstractContainer
        {
            public SnackBarContent(IWebDriver webDriver, By locator)
                : base(webDriver, locator)
            {
                this.CloseButton = this.Find<Element>(By.CssSelector(".mat-mdc-button"));
            }

            public Element CloseButton { get; private set; }

            public override ValidationResult IsLoaded()
            {
                if (!this.Displayed)
                {
                    return new ValidationResult { Passed = false, Message = $"Container not displayed\n Path: {this.Path}" };
                }

                return new ValidationResult { Passed = true, Message = "Ok" };
            }
        }
    }
}
