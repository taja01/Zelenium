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
        protected readonly string Url;

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, IRouteBuilder<TEnum> routeBuilder, TEnum page)
            : base(webDriver, locator)
        {
            this.Url = routeBuilder.GetUrl(page);
        }

        protected AbstractLoadableContainer(IWebDriver webDriver, By locator, string url) : base(webDriver, locator)
        {
            this.Url = url;
        }

        public virtual void Load()
        {
            this.webDriver.Url = this.Url;
        }

        public virtual void Load(string urlSegmens)
        {

            this.webDriver.Url = $"{this.Url}/{urlSegmens}";
        }
    }
}
