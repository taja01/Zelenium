using OpenQA.Selenium;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver.Types
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
