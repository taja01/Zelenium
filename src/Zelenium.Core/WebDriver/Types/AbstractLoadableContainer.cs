using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver.Types
{
    public abstract class AbstractLoadableContainer(ILogger logger, IWebDriver webDriver, By locator, string url) : AbstractContainer(logger, webDriver, locator), ILoadableContainer
    {
        public readonly string Url = url;

        /// <summary>
        /// Load Page, Url set with constructor OR with RouterBuilder
        /// </summary>
        public virtual void Load()
        {
            this.webDriver.Url = this.Url;
        }

        /// <summary>
        /// Load Page, Url set with constructor OR with RouterBuilder
        /// </summary>
        /// <param name="urlSegmens">Extra url part. You have to add separator if necessary</param>
        public virtual void Load(string urlSegmens)
        {
            this.webDriver.Url = $"{this.Url}{urlSegmens}";
        }
    }

    public abstract class AbstractLoadableContainer<TEnum> : AbstractContainer, ILoadableContainer
    {
        public readonly string Url;
        protected AbstractLoadableContainer(ILogger logger, IWebDriver webDriver, By locator, IRouteBuilder<TEnum> routeBuilder, TEnum page)
            : base(logger, webDriver, locator)
        {
            this.Url = routeBuilder.GetUrl(page);
        }

        protected AbstractLoadableContainer(ILogger logger, IWebDriver webDriver, By locator, string url) : base(logger, webDriver, locator)
        {
            this.Url = url;
        }

        /// <summary>
        /// Load Page, Url set with constructor OR with RouterBuilder
        /// </summary>
        public virtual void Load()
        {
            this.webDriver.Url = this.Url;
        }

        /// <summary>
        /// Load Page, Url set with constructor OR with RouterBuilder
        /// </summary>
        /// <param name="urlSegmens">Extra url part. You have to add separator if necessary</param>
        public virtual void Load(string urlSegmens)
        {
            this.webDriver.Url = $"{this.Url}{urlSegmens}";
        }
    }
}
