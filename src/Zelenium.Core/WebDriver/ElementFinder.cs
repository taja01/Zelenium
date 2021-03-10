using System;
using System.Collections;
using OpenQA.Selenium;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver
{
    internal class ElementFinder : IElementFinder
    {
        private readonly ISearchContext searchContext;
        private IWebElement cachedWebElement;
        private readonly TimeSpan timeOut;
        private readonly By locator;
        private readonly IElementFinder finder;
        private readonly int index;
        private readonly bool isShadow;

        public ElementFinder(ISearchContext searchContext, IElementFinder finder, By locator, TimeSpan? timeOut = null, bool isShadow = false)
        {
            this.searchContext = searchContext;
            this.locator = locator;
            this.timeOut = timeOut ?? TimeSpan.FromSeconds(5);
            this.finder = finder;
            this.index = -1;
            this.isShadow = isShadow;
            this.Path = this.GetPath();
        }

        public ElementFinder(ISearchContext searchContext, IElementFinder finder, IWebElement cacheElement, By locator, int index, TimeSpan? timeOut = null, bool isShadow = false)
        {
            this.searchContext = searchContext;
            this.locator = locator;
            this.timeOut = timeOut ?? TimeSpan.FromSeconds(5);
            this.finder = finder;
            this.cachedWebElement = cacheElement;
            this.index = index;
            this.isShadow = isShadow;
            this.Path = this.GetPath();
        }

        public string Path { get; }

        /// <summary>
        /// Get WebElement if exist in the DOM
        /// </summary>
        /// <returns></returns>
        public IWebElement GetWebElement()
        {
            if (this.IsCashValid() || this.Present())
            {
                return this.cachedWebElement;
            }

            throw new NoSuchElementException($"Element not found {this.Path}");
        }

        /// <summary>
        /// Get WebElement if displayed
        /// </summary>
        /// <returns></returns>
        public IWebElement GetDisplayedWebElement()
        {
            if (this.IsCashValid() || this.Displayed())
            {
                return this.cachedWebElement;
            }

            throw new NoSuchElementException($"Element not found {this.Path}");
        }

        public bool Present(TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? this.timeOut)
                .IgnoreExceptionTypes(typeof(NoSuchElementException))
                .Success(() => this.TryFindElement(out _, timeout));
        }

        public bool Displayed(TimeSpan? timeout = null)
        {
            return Wait.Initialize()
               .Timeout(timeout ?? this.timeOut)
               .IgnoreExceptionTypes(typeof(NoSuchElementException))
               .Success(() =>
               {
                   return this.TryFindElement(out var element, timeout) && element.Displayed;
               });
        }

        private bool TryFindElement(out IWebElement webElement, TimeSpan? timeout = null)
        {
            webElement = null;

            IWebElement FindSingleElement()
            {
                return this.cachedWebElement = this.finder == null
                    ? this.searchContext.FindElement(this.locator)
                    : this.finder.GetWebElement().FindElement(this.locator);
            }

            IWebElement FindMultiSingleElement()
            {
                var list = this.finder == null
                    ? this.searchContext.FindElements(this.locator)
                    : this.finder.GetWebElement().FindElements(this.locator);

                if (this.index >= list.Count)
                {
                    throw new IndexOutOfRangeException($"List has only '{list.Count}' elements. Your index is: '{this.index}'");
                }

                return this.cachedWebElement = list[this.index];
            }

            IWebElement FindShadowSingleElement()
            {
                this.ValidateShadowConditions();

                this.cachedWebElement = (IWebElement)((IJavaScriptExecutor)this.searchContext)
                    .ExecuteScript(BaseQueries.GetShadowElement(this.locator.Criteria).Script, this.finder.GetWebElement());

                return this.cachedWebElement;
            }

            IWebElement FindShadowMultiSingleElement()
            {
                this.ValidateShadowConditions();

                var list = ((IJavaScriptExecutor)this.searchContext)
                   .ExecuteScript(BaseQueries.GetShadowElements(this.locator.Criteria).Script, this.finder.GetWebElement());

                var counter = 0;
                if (list is IEnumerable myList)
                {

                    foreach (object element in myList)
                    {
                        if (this.index == counter)
                        {
                            return this.cachedWebElement = (IWebElement)element;
                        }
                        else
                        {
                            counter++;
                        }
                    }
                }
                else
                {
                    throw new NoSuchElementException("List is empty");
                }

                throw new IndexOutOfRangeException($"List has only '{counter}' elements. Your index is: '{this.index}'");

            }

            webElement = Wait.Initialize()
                .IgnoreExceptionTypes(typeof(NoSuchElementException))
                .Timeout(timeout ?? this.timeOut)
                .Until(() =>
                {
                    if (this.isShadow)
                    {
                        return this.index >= 0
                       ? FindShadowMultiSingleElement()
                       : FindShadowSingleElement();
                    }
                    else
                    {
                        return this.index >= 0
                        ? FindMultiSingleElement()
                        : FindSingleElement();
                    }
                });

            return webElement != null;
        }

        private bool IsCashValid()
        {
            try
            {
                return this.cachedWebElement != null && this.cachedWebElement.Enabled && true;

            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        private string GetPath()
        {
            if (this.finder == null)
            {
                return this.locator == null ? string.Empty : this.locator.ToString();
            }
            return this.index > -1 ? $"{this.finder.Path} {this.locator}[{this.index}]" : $"{this.finder.Path} {this.locator}";
        }

        private void ValidateShadowConditions()
        {
            if (this.finder == null)
            {
                throw new WebDriverException("You need to find the shadow 'parent' elemet first!");
            }

            if (this.locator.Mechanism != "css selector")
            {
                throw new WebDriverException("Only css selector allow to find element under shadow root");
            }
        }
    }
}
