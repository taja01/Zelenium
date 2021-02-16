using System;
using OpenQA.Selenium;

namespace Zelenium.Core.Interfaces
{
    public interface IElementFinder
    {
        string Path { get; }
        bool Present(TimeSpan? timeout = null);
        bool Displayed(TimeSpan? timeout = null);
        IWebElement GetDisplayedWebElement();
        IWebElement GetWebElement();
    }
}
