using System;
using OpenQA.Selenium;
using ZeleniumFramework.Config;
using ZeleniumFramework.Model;

namespace ZeleniumFramework.WebDriver
{
    public abstract class AbstractLoadableContainer : AbstractContainer
    {
        private readonly string url;

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, string url) : base(webDriver, locator)
        {
            this.url = url;
        }

        public virtual void Load()
        {
            this.webDriver.Url = this.url;
        }

        public void WaitForLoad(TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message("Cannot load page")
                .Timeout(timeout ?? TimeConfig.LongTimeout)
                .Until(this.IsLoaded, t => t.Passed, t => t.Message);
        }

        public abstract ValidationResult IsLoaded();
    }
}
