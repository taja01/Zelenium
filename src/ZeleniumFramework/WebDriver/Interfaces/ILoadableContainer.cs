using System;
using ZeleniumFramework.Model;

namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface ILoadableContainer
    {
        void Load();
        void WaitForLoad(TimeSpan? timeout = null);
        ValidationResult IsLoaded();
    }
}
