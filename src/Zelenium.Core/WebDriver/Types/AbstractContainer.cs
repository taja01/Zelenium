using System;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;
using Zelenium.Core.Model;

namespace Zelenium.Core.WebDriver.Types
{
    public abstract class AbstractContainer(ILogger logger, IWebDriver webDriver, By locator) : AbstractElement(logger, webDriver, locator)
    {
        /// <summary>
        /// Finds a web element based on a specific locator and optional timeout.
        /// </summary>
        /// <param name="locator">The location strategy to use when searching for the element.</param>
        /// <param name="timeout">The maximum amount of time to wait for the element to be found. Defaults to null if not specified.</param>
        /// <typeparam name="T">The type of element container.</typeparam>
        /// <returns>Returns a new instance of the type specified by the generic parameter T, where T must implement IElementContainer.</returns>
        /// <remarks>
        /// The generic type T must have a constructor that takes a ILogger, IWebDriver and By as parameters, in that order.
        /// </remarks>
        protected T Find<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            var elementContainer = (T)Activator.CreateInstance(typeof(T), this.logger, this.webDriver, locator);
            elementContainer.Finder = new ElementFinder(this.webDriver, this.Finder, locator, timeout);

            return elementContainer;
        }

        /// <summary>
        /// Finds a shadow web element based on a specific locator and optional timeout.
        /// </summary>
        /// <param name="locator">The location strategy to use when searching for the shadow element.</param>
        /// <param name="timeout">The maximum amount of time to wait for the shadow element to be found. Defaults to null if not specified.</param>
        /// <typeparam name="T">The type of element container.</typeparam>
        /// <returns>Returns a new instance of the type specified by the generic parameter T, where T must implement IElementContainer.</returns>
        /// <remarks>
        /// The generic type T must have a constructor that takes a ILogger, IWebDriver and By as parameters, in that order.
        /// The method will return a shadow web element, which is an instance of type T.
        /// This method specifically deals with finding elements within the shadow DOM of a web page.
        /// </remarks>
        protected T FindShadow<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            var elementContainer = (T)Activator.CreateInstance(typeof(T), this.logger, this.webDriver, locator);
            elementContainer.Finder = new ElementFinder(this.webDriver, this.Finder, locator, timeout, true);

            return elementContainer;
        }

        /// <summary>
        /// Finds a list of web elements based on a specific locator and optional timeout.
        /// </summary>
        /// <param name="locator">The location strategy to use when searching for the elements.</param>
        /// <param name="timeout">The maximum amount of time to wait for the elements to be found. Defaults to null if not specified.</param>
        /// <typeparam name="T">The type of element container.</typeparam>
        /// <returns>Returns a new instance of the type ElementList&lt;T&gt;, where T must implement IElementContainer.</returns>
        /// <remarks>
        /// The generic type T must have a constructor that takes a ILogger, IWebDriver and By as parameters, in that order. 
        /// The method will return a list of elements, each of which is an instance of type T.
        /// </remarks>
        protected ElementList<T> Finds<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            return new ElementList<T>(this.logger, this.webDriver, this.Finder, locator, timeout);
        }

        /// <summary>
        /// Finds a list of shadow web elements based on a specific locator and optional timeout.
        /// </summary>
        /// <param name="locator">The location strategy to use when searching for the shadow elements.</param>
        /// <param name="timeout">The maximum amount of time to wait for the shadow elements to be found. Defaults to null if not specified.</param>
        /// <typeparam name="T">The type of element container.</typeparam>
        /// <returns>Returns a new instance of ElementList&lt;T&gt;, where T must implement IElementContainer. Each element in the list is an instance of T.</returns>
        /// <remarks>
        /// The generic type T must have a constructor that takes ILogger, IWebDriver and By as parameters, in that order. 
        /// This method specifically deals with finding a list of elements within the shadow DOM of a web page.
        /// </remarks>
        protected ElementList<T> FindsShadow<T>(By locator, TimeSpan? timeout = null) where T : IElementContainer
        {
            return new ElementList<T>(this.logger, this.webDriver, this.Finder, locator, timeout, true);
        }

        // <summary>
        /// Checks a series of conditions and provides a validation result.
        /// </summary>
        /// <param name="checks">An array of tuples where each tuple represents a condition to check and a corresponding description.</param>
        /// <returns>A ValidationResult object. If all conditions are met, ValidationResult will indicate success, else it will contain the description of the failed check.</returns>
        protected ValidationResult CheckAllLoaded((Func<bool> Condition, string Description)[] checks)
        {
            foreach (var (Condition, Description) in checks)
            {
                if (!Condition())
                {
                    return new ValidationResult { Passed = false, Message = $"{Description}: {this.Path}" };
                }
            }

            return new ValidationResult { Passed = true, Message = "Ok" };
        }

        /// <summary>
        /// Waits for a page to load within a specified timeout period.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the page to load. Defaults to a predefined long timeout (30 seconds) if not provided.</param>
        /// <remarks>
        /// Calls the IsLoaded method to validate if the page has loaded successfully. If the page does not load within the timeout, an exception is thrown with the message "Cannot load page".
        /// </remarks>
        public virtual void WaitForLoad(TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message("Cannot load page")
                .Timeout(timeout ?? TimeConfig.LongTimeout)
                .Until(this.IsLoaded, t => t.Passed, t => t.Message);
        }

        // <summary>
        /// A method to validate if a page is correctly loaded.
        /// </summary>
        /// <remarks>
        /// You can either use CheckAllLoaded method passing the necessary conditions as parameters or override this method to implement your own custom validation logic to verify the page loading.
        /// </remarks>
        /// <returns>Returns a ValidationResult object reflecting the status of the page load validation. If the page is correctly loaded, ValidationResult will indicate success, else it will contain an appropriate failure message.</returns>
        public abstract ValidationResult IsLoaded();
    }
}
