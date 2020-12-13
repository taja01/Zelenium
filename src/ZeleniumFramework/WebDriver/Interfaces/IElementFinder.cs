﻿using OpenQA.Selenium;
using System;

namespace ZeleniumFramework.WebDriver.Interfaces
{
    public interface IElementFinder
    {
        string Path { get; }
        bool Present(TimeSpan? timeout = null);
        bool Displayed(TimeSpan? timeout = null);
        IWebElement WebElement();
    }
}