using System;
using OpenQA.Selenium;

namespace Zelenium.Core.WebDriver.Interfaces
{
    public interface IElementFinder
    {
        string Path { get; }
        bool Present(TimeSpan? timeout = null);
        bool Displayed(TimeSpan? timeout = null);
        IWebElement WebElement();
    }
}
