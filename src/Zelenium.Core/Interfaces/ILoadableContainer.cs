using System;
using Zelenium.Core.Model;

namespace Zelenium.Core.Interfaces
{
    public interface ILoadableContainer
    {
        void Load();
        void Load(string urlSegmens);
        void WaitForLoad(TimeSpan? timeout = null);
        ValidationResult IsLoaded();
    }
}
