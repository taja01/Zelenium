using OpenQA.Selenium;
using ZeleniumFramework.WebDriver.Interfaces;

namespace ZeleniumFramework.WebDriver
{
    public abstract class AbstractLoadableContainer : AbstractContainer, ILoadableContainer
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
    }

    public abstract class AbstractLoadableContainer<TEnum> : AbstractContainer, ILoadableContainer
    {
        private readonly string url;

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, IRouteBuilder<TEnum> routeBuilder, TEnum page)
            : base(webDriver, locator)
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
