using System;
using System.Collections.Generic;
using System.Linq;
using Zelenium.Core.Config;
using Zelenium.Core.Interfaces;

namespace Zelenium.Core.WebDriver
{
    public class ClassAttribute
    {
        private readonly Attributes attribute;
        private const string ATTRIBUTE_NAME = "class";
        private readonly string path;
        public ClassAttribute(IElementFinder finder)
        {
            this.path = finder.Path;
            this.attribute = new Attributes(finder);
        }

        public bool Has(string className)
        {
            return this.attribute.Get(ATTRIBUTE_NAME)?.Split(' ').Contains(className) ?? false;
        }

        public IList<string> Get()
        {
            return this.attribute.Get(ATTRIBUTE_NAME)?.Split(' '); ;
        }

        public bool HasWithin(string className, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.Has(className));
        }

        public bool HasRemovedWithin(string className, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => !this.Has(className));
        }

        public void WaitForClass(string className, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message($"Element has NOT got '{className}' class\n Path: {this.path}")
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => this.Has(className));
        }

        public void WaitForRemoveClass(string className, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message($"Element still has got '{className}' class\n Path: {this.path}")
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => !this.Has(className));
        }
    }
}
