using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver.Types;

namespace MaterialAngular.PageObjects
{
    public class TabsPage : AbstractLoadableContainer
    {
        private Element tab3;
        private Element tab2;
        private Element tab1;

        public TabsPage(ILogger logger, IWebDriver webDriver) : base(logger, webDriver, By.CssSelector(".docs-component-sidenav-content"), "https://material.angular.io/components/tabs/overview")
        {
            //this.Header = this.Find<Element>(By.CssSelector(".mat-mdc-tab-links"));
            this.Tabs = this.Finds<Element>(By.CssSelector(".docs-component-viewer-section-tab"));
        }

        private ElementList<Element> Tabs { get; set; }
        public Element Header { get; private set; }
        public Element Tab1
        {
            get
            {
                this.tab1 ??= this.Tabs[0];
                return this.tab1;
            }
        }

        public Element Tab2
        {
            get
            {
                this.tab2 ??= this.Tabs[1];
                return this.tab2;
            }
        }

        public Element Tab3
        {
            get
            {
                this.tab3 ??= this.Tabs[2];
                return this.tab3;
            }
        }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }
            //if (!this.Header.Displayed)
            //{
            //    return new ValidationResult { Passed = false, Message = $"Header missing \n{this.Header.Path}" };
            //}
            if (this.Tabs.Count != 3)
            {
                return new ValidationResult { Passed = false, Message = $"Unexpected number of tabs \n{this.Tabs.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
