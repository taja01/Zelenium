using System;
using Zelenium.Core.Config;
using Zelenium.Core.Exceptions;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver
{
    /// <summary>
    /// Get html element's attribute
    /// </summary>
    public class Attributes
    {
        private readonly IElementFinder finder;

        public Attributes(IElementFinder finder)
        {
            this.finder = finder;
        }

        /// <summary>
        /// Get selected attribute value
        /// </summary>
        /// <param name="name"></param>
        /// <returns>string</returns>
        public string Get(string name)
        {
            //return Retry.Do<StaleElementReferenceException, string>(() => _finder.WebElement().GetAttribute(name));
            return this.finder.GetWebElement().GetAttribute(name);
        }

        /// <summary>
        /// Return true or false that given attribute is exist
        /// </summary>
        /// <param name="name">Attribute name</param
        /// <returns>boolean</returns>
        public bool Has(string name)
        {
            return this.Get(name) != null;
        }

        /// <summary>
        /// Return true or false that given attribute and value are exist
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Expected value</param>
        /// <returns></returns>
        public bool Has(string name, string value)
        {
            return this.Get(name)?.Contains(value) ?? false;
        }

        /// <summary>
        /// Return true or false that given attribute and value are exist in time
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Expected value</param>
        /// <param name="timeout">Maximum waiting time. Default 5s.</param>
        /// <returns>boolean</returns>
        public bool HasWithin(string name, string value, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.Has(name, value));
        }

        /// <summary>
        /// Return true or false that given attribute is exist in time
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="timeout">Maximum waiting time. Default 5s.</param>
        /// <returns>boolean</returns>
        public bool HasWithin(string name, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.Has(name));
        }

        /// <summary>
        /// Wait for attribute. If the attribute not present then exception thrown
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="timeout">Maximum waiting time. Default 5s.</param>
        /// <exception cref="AttributeNotExistException">Throw when attribute not exist</exception>
        public void WaitFor(string name, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Message($"No '{name}' attribute found. {this.finder.Path}")
                .Throw<AttributeNotExistException>()
                .Until(() => this.Has(name));
        }

        /// <summary>
        /// Wait for attribute with given value. If the attribute not present then exception thrown
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Expected value</param>
        /// <param name="timeout">Maximum waiting time. Default 5s.</param>
        /// <exception cref="AttributeNotExistException">Throw when attribute not exist with value</exception>

        public void WaitFor(string name, string value, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Message($"The '{name}' attribute did not contain the '{value}' value within timeout. {this.finder.Path}")
                .Throw<AttributeNotExistException>()
                .Until(() => this.Has(name, value));
        }
    }
}
