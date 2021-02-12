using OpenQA.Selenium;
using Zelenium.Core.Model;
using Zelenium.Core.WebDriver;

namespace MaterialAngular.PageObjects
{
    public class TabsPage : AbstractLoadableContainer
    {
        private Element tab3;
        private Element tab2;
        private Element tab1;

        public TabsPage(IWebDriver webDriver) : base(webDriver, By.CssSelector(".docs-component-sidenav-content"), "https://material.angular.io/components/tabs/overview")
        {
            this.Header = this.Find<Element>(By.CssSelector("header"));
            this.Tabs = this.Finds<Element>(By.CssSelector(".docs-example-viewer-body .mat-tab-label"));
        }

        private ElementList<Element> Tabs { get; set; }
        public Element Header { get; private set; }
        public Element Tab1
        {
            get
            {
                if (this.tab1 == null)
                {
                    this.tab1 = this.Tabs[0];
                }
                return this.tab1;
            }
        }

        public Element Tab2
        {
            get
            {
                if (this.tab2 == null)
                {
                    this.tab2 = this.Tabs[1];
                }
                return this.tab2;
            }
        }

        public Element Tab3
        {
            get
            {
                if (this.tab3 == null)
                {
                    this.tab3 = this.Tabs[2];
                }
                return this.tab3;
            }
        }

        public override ValidationResult IsLoaded()
        {
            if (!this.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Page load failed \n{this.Path}" };
            }
            if (!this.Header.Displayed)
            {
                return new ValidationResult { Passed = false, Message = $"Header missing \n{this.Header.Path}" };
            }
            if (this.Tabs.Count != 3)
            {
                return new ValidationResult { Passed = false, Message = $"Unexpected number of tabs \n{this.Tabs.Path}" };
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }
    }
}
