using OpenQA.Selenium;
using ZeleniumFramework.Model;
using ZeleniumFramework.WebDriver;

namespace MaterialAngular.PageObjects
{
    public class SnackBarPage : AbstractLoadableContainer
    {
        public SnackBarPage(IWebDriver webDriver) : base(webDriver, null, "https://material.angular.io/components/snack-bar/overview")
        {
            this.ShowSnackBarButton = this.Find<Element>(By.CssSelector("snack-bar-overview-example > button"));
            this.SnackBar = this.Find<Element>(By.CssSelector(".mat-simple-snackbar-action"));
        }

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
