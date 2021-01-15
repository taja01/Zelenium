using System;
using ZeleniumFramework.Model;

namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface ILoadableContainer
    {
        abstract void Load();
        void WaitForLoad(TimeSpan? timeout = null);
        abstract ValidationResult IsLoaded();
    }
}
