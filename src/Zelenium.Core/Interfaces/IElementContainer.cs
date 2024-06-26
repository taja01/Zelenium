﻿using System;
using System.Drawing;
using OpenQA.Selenium;
using Zelenium.Core.Enums;
using Zelenium.Core.Helper;
using Zelenium.Core.Model;
using Zelenium.Core.Utils;
using Zelenium.Core.WebDriver;

namespace Zelenium.Core.Interfaces
{
    public interface IElementContainer
    {
        IElementFinder Finder { get; set; }
        IWebElement WebElement { get; }
        IWebElement DisplayedWebElement { get; }
        Point Location { get; }
        bool Displayed { get; }
        bool Present { get; }
        bool DisplayedNow { get; }
        bool PresentNow { get; }
        Color BackgroundColor { get; }
        ClassAttribute Class { get; }
        Attributes Attributes { get; }
        string Path { get; }
        JavaScriptExecutor JavaScriptExecutor { get; }

        ValidationResult AreAllSubElementsDisplayed();
        bool IsDisappeared(TimeSpan? timeout = null);
        void Click(ClickMethod clickMethod);
        void WaitUntilDisappear(string errorMessage, TimeSpan? timeout = null);
        void WaitUntilDisplay(string errorMessage, TimeSpan? timeout = null);
        void ExecuteScript(JsQuery script);
        public T ExecuteScript<T>(JsQuery script);
        void DragAndDrop(int xAxis, int yAxis);
        void Swipe(int xAxis);
        void Scroll();
        string GetComputedStyle(string style, string pseudo = null);
        string GetCssValue(string propertyName);

        bool IsInViewPortWithin(TimeSpan? timeout = null);
        void WaitUntilInViewPort(string elementName, TimeSpan? timeout = null);
        bool IsInViewPort();
    }
}
