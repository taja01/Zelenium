using System;
using OpenQA.Selenium;
using ZeleniumFramework.WebDriver.Interfaces;

namespace ZeleniumFramework.WebDriver
{
    public abstract class AbstractLoadableContainer : AbstractContainer
    {
        private readonly string url;

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, IRouteBuilder routeBuilder, Enum page) : base(webDriver, locator)
        {
            this.url = routeBuilder.GetUrl(page);
        }

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, string url) : base(webDriver, locator)
        {
            this.url = url;
        }

        public virtual void Load()
        {
            this.webDriver.Url = this.url;
        }
    }
}
