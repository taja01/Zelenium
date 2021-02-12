using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Zelenium.Core.WebDriver.Interfaces;

namespace Zelenium.Core.WebDriver
{
    public class ElementList<T> : IEnumerable<T> where T : IElementContainer
    {
        private readonly IWebDriver webDriver;
        private readonly IElementFinder finder;
        private readonly By locator;
        private readonly TimeSpan timeout;

        public ElementList(IWebDriver webDriver, IElementFinder finder, By locator, TimeSpan? timeout)
        {
            this.webDriver = webDriver;
            this.finder = finder;
            this.locator = locator;
            this.timeout = timeout ?? TimeSpan.FromSeconds(5);
        }

        public string Path => $"{this.finder.Path} {this.locator}";
        public virtual int Count => this.Elements.Count();
        public T this[int index] => this.GetElement(index);

        public List<T> Elements
        {
            get
            {
                try
                {
                    return Wait.Initialize()
                        .Timeout(this.timeout)
                        .Until(() => this.FindElements());
                }
                catch (WebDriverTimeoutException)
                {
                    return new List<T>();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Elements.Any())
            {
                return this.Elements.GetEnumerator();
            }
            throw new NoSuchElementException($"Cannot find {typeof(T).Name} elements.{Environment.NewLine}");
        }

        private List<T> FindElements()
        {
            var elements = this.FindMultiElements();

            if (!elements.Any())
            {
                return null;
            }

            var index = 0;
            var typedList = elements.Select(element =>
            {
                var elementContainer = (T)Activator.CreateInstance(typeof(T), this.webDriver, this.locator);
                elementContainer.Finder = new ElementFinder(this.webDriver, this.finder, element, this.locator, index++);
                return elementContainer;
            });

            return typedList.ToList();
        }

        private IReadOnlyCollection<IWebElement> FindMultiElements()
        {
            return this.finder == null
                ? this.webDriver.FindElements(this.locator)
                : this.finder.WebElement().FindElements(this.locator);
        }

        private T GetElement(int index)
        {
            var elements = this.Elements;
            if (elements.Count > index)
            {
                return elements[index];
            }
            throw new IndexOutOfRangeException(
                $"Index {index} is out of range. {typeof(T).Name} list has {elements.Count} items.{Environment.NewLine}");
        }
    }
}
