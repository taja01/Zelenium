using System;
using ZeleniumFramework.Config;

namespace ZeleniumFramework.WebDriver
{
    public class Attributes
    {
        private readonly IElementFinder finder;

        public Attributes(IElementFinder finder)
        {
            this.finder = finder;
        }

        public string Get(string name)
        {
            //return Retry.Do<StaleElementReferenceException, string>(() => _finder.WebElement().GetAttribute(name));
            return this.finder.WebElement().GetAttribute(name);
        }

        public bool Has(string name)
        {
            return this.Get(name) != null;
        }

        public bool Has(string name, string value)
        {
            return this.Get(name)?.Contains(value) ?? false;
        }

        public bool HasWithin(string name, string value, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.Has(name, value));
        }

        public bool HasWithin(string name, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.Has(name));
        }

        public void WaitFor(string name, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Message($"No '{name}' attribute found. {this.finder.Path}")
                .Until(() => this.Has(name));
        }

        public void WaitFor(string name, string value, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Message($"The '{name}' attribute did not contain the '{value}' value within timeout. {this.finder.Path}")
                .Until(() => this.Has(name, value));
        }
    }
}
