﻿using OpenQA.Selenium;

namespace ZeleniumFramework.WebDriver.Types
{
    public class Image : Element
    {
        public Image(IWebDriver webDriver, By by) : base(webDriver, by)
        {
        }

        public string AlternateText => this.Attributes.Get("alt");
        public string Source => this.Attributes.Get("src");
    }
}
