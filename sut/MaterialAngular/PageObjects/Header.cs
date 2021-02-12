using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver;


namespace MaterialAngular.PageObjects
{
    public class Header : AbstractContainer
    {
        public Header(IWebDriver driver, By selector)
            : base(driver, selector)
        {
            this.MaterialButton = this.Find<Element>(By.CssSelector("a[href='/']"));
            this.ComponentButton = this.Find<Element>(By.CssSelector("a[href='/components']"));
            this.CdkButton = this.Find<Element>(By.CssSelector("a[href='/cdk']"));
        }
        public Element MaterialButton { get; private set; }
        public Element ComponentButton { get; private set; }
        public Element CdkButton { get; private set; }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Container not displayed\n Path: {this.Path}" };
            }
            if (!this.MaterialButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"MaterialButton not displayed\n Path: {this.MaterialButton.Path}" };
            }
            if (!this.ComponentButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"ComponentButton not displayed\n Path: {this.ComponentButton.Path}" };
            }
            if (!this.CdkButton.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"CdkButton not displayed\n Path: {this.CdkButton.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" }; ;
        }
    }
}
