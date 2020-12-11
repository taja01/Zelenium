using OpenQA.Selenium;
using ZeleniumFramework.Enums;
using System;
using System.Drawing;

namespace ZeleniumFramework.WebDriver
{
    public interface IElement
    {
        IElementFinder Finder { get; set; }
        IWebElement WebElement { get; }
        Point Location { get; }
        bool Displayed { get; }
        bool Present { get; }
        bool DisplayedNow { get; }
        bool PresentNow { get; }
        Color BackgroundColor { get; }
        ClassAttribute Class { get; }
        Attributes Attributes { get; }
        public string Path { get; }

        void Click(ClickMethod clickMethod);
        void WaitUntilDisappear(string errorMessage, TimeSpan? timeout = null);
        void WaitUntilDisplay(string errorMessage, TimeSpan? timeout = null);
        void ExecuteScript(string script);
        void DragAndDrop(int xAxis, int yAxis);
        void Swipe(int xAxis);
        void Scroll();
        string GetComputedStyle(string style, string pseudo = null);
        string GetCssValue(string propertyName);
    }
}
