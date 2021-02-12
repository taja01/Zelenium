using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver;

namespace MaterialAngular.PageObjects
{
    public class SnackBarPage : AbstractLoadableContainer
    {
        public SnackBarPage(IWebDriver webDriver) : base(webDriver, null, "https://material.angular.io/components/snack-bar/overview")
        {
            this.ShowSnackBarButton = this.Find<Element>(By.CssSelector("snack-bar-overview-example > button"));
            this.SnackBar = this.Find<Element>(By.CssSelector(".mat-simple-snackbar-action"));
            this.Header = this.Find<Header>(By.CssSelector(".docs-navbar-header"));

        }

        public Header Header { get; private set; }
        public Element ShowSnackBarButton { get; private set; }
        public Element SnackBar { get; private set; }

        public override ValidationResult IsLoaded()
        {

            if (!this.ShowSnackBarButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"ShowSnackBar not displayed\n Path: {this.ShowSnackBarButton.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
