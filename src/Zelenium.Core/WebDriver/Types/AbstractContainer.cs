using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;
using Zelenium.Core.Model;

namespace Zelenium.Core.WebDriver.Types
{
    public abstract class AbstractContainer : AbstractElement
    {
        protected AbstractContainer(ILogger logger, IWebDriver webDriver, By locator) : base(logger, webDriver, locator)
        {
        }

        protected T Find<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            var elementContainer = (T)Activator.CreateInstance(typeof(T), this.logger, this.webDriver, locator);
            elementContainer.Finder = new ElementFinder(this.webDriver, this.Finder, locator, timeout);

            return elementContainer;
        }

        protected T FindShadow<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            var elementContainer = (T)Activator.CreateInstance(typeof(T), this.logger, this.webDriver, locator);
            elementContainer.Finder = new ElementFinder(this.webDriver, this.Finder, locator, timeout, true);

            return elementContainer;
        }

        protected ElementList<T> Finds<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            return new ElementList<T>(this.logger, this.webDriver, this.Finder, locator, timeout);
        }

        protected ElementList<T> FindsShadow<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            return new ElementList<T>(this.logger, this.webDriver, this.Finder, locator, timeout, true);
        }

        public virtual void WaitForLoad(TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message("Cannot load page")
                .Timeout(timeout ?? TimeConfig.LongTimeout)
                .Until(this.IsLoaded, t => t.Passed, t => t.Message);
        }

        public abstract ValidationResult IsLoaded();
    }
}
