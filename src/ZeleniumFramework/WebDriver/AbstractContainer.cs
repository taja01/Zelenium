using System;
using OpenQA.Selenium;

namespace ZeleniumFramework.WebDriver
{
    public abstract class AbstractContainer : AbstractElement
    {
        protected AbstractContainer(IWebDriver webDriver, By locator) : base(webDriver, locator)
        {
        }

        protected T Find<T>(By locator, TimeSpan? timeout = null) where T : IElement
        {
            var elementContainer = (T)Activator.CreateInstance(typeof(T), this.webDriver, locator);
            elementContainer.Finder = new ElementFinder(this.webDriver, this.Finder, locator, timeout);

            return elementContainer;
        }

        protected ElementList<T> Finds<T>(By locator, TimeSpan? timeout = null) where T : IElement
        {
            return new ElementList<T>(this.webDriver, this.Finder, locator, timeout);
        }
    }
}
