﻿using System;
using OpenQA.Selenium;
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

        public ElementFinder(ISearchContext searchContext, IElementFinder finder, By locator, TimeSpan? timeOut = null)
        {
            this.searchContext = searchContext;
            this.locator = locator;
            this.timeOut = timeOut ?? TimeSpan.FromSeconds(5);
            this.finder = finder;
            this.index = -1;
            this.Path = this.GetPath();
        }

        public ElementFinder(ISearchContext searchContext, IElementFinder finder, IWebElement cacheElement, By locator, int index, TimeSpan? timeOut = null)
        {
            this.searchContext = searchContext;
            this.locator = locator;
            this.timeOut = timeOut ?? TimeSpan.FromSeconds(5);
            this.finder = finder;
            this.cachedWebElement = cacheElement;
            this.index = index;
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
                    : this.finder.GetDisplayedWebElement().FindElement(this.locator);
            }

            IWebElement FindMultiSingleElement()
            {
                var list = this.finder == null
                    ? this.searchContext.FindElements(this.locator)
                    : this.finder.GetDisplayedWebElement().FindElements(this.locator);

                if (this.index >= list.Count)
                {
                    throw new NoSuchElementException("What is this?");
                }

                return this.cachedWebElement = list[this.index];
            }

            webElement = Wait.Initialize()
                .IgnoreExceptionTypes(typeof(NoSuchElementException))
                .Timeout(timeout ?? this.timeOut)
                .Until(() =>
                {
                    var element = this.index >= 0
                    ? FindMultiSingleElement()
                    : FindSingleElement();

                    return element;
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
    }
}