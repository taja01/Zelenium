using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace TestPage.Pages
{
    public class NavBar : AbstractContainer
    {
        public NavBar(ILogger logger, IWebDriver driver, By selector)
            : base(logger, driver, selector)
        {
            this.HomeButton = this.Find<Element>(By.CssSelector("a[href='index.html']"));
            this.LoginButton = this.Find<Element>(By.CssSelector("a[href='login.html']"));
        }
        public Element HomeButton { get; private set; }
        public Element LoginButton { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Container not displayed\n Path: {this.Path}" };
            }
            if (!this.HomeButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"HomeButton not displayed\n Path: {this.HomeButton.Path}" };
            }
            if (!this.LoginButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"LoginButton not displayed\n Path: {this.LoginButton.Path}" };
            }


            return new ValidationResult { Passed = true, Message = "Ok" }; ;
        }
    }
}
