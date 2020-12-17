using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ZeleniumFramework.Config;
using ZeleniumFramework.Enums;
using ZeleniumFramework.Utils;
using ZeleniumFramework.WebDriver.Interfaces;

namespace ZeleniumFramework.WebDriver
{
    public abstract class AbstractElement : IElementContainer
    {
        protected readonly IWebDriver webDriver;
        protected AbstractElement(IWebDriver webDriver, By locator = null)
        {
            this.webDriver = webDriver;
            if (locator != null)
            {
                this.Finder = new ElementFinder(this.webDriver, null, locator);
            }
        }

        public IElementFinder Finder { get; set; }
        public IWebElement WebElement => this.Finder.WebElement();
        public Point Location => this.Do(() => this.Finder.WebElement().Location);
        public bool Displayed => this.Finder.Displayed();
        public bool Present => this.Finder.Present();
        public bool DisplayedNow => this.Finder.Displayed(TimeSpan.Zero);
        public bool PresentNow => this.Finder.Present(TimeSpan.Zero);
        public Color BackgroundColor => this.GetBackgroundColor();
        public ClassAttribute Class => new ClassAttribute(this.Finder);
        public Attributes Attributes => new Attributes(this.Finder);
        public string Path => this.Finder.Path;

        public void Click(ClickMethod clickMethod = ClickMethod.Default)
        {
            switch (clickMethod)
            {
                case ClickMethod.Default:
                    {
                        if (this.Displayed)
                        {
                            this.Scroll();
                            this.WebElement.Click();
                        }
                        else
                        {
                            throw new ElementNotVisibleException($"ELement not visible - not possible to click. Path: {this.Path}");
                        }
                        break;
                    }
                case ClickMethod.Javascript: this.ExecuteScript("arguments[0].click();"); break;
                case ClickMethod.NewTab: this.ExecuteScript("window.open(arguments[0], '_blank')"); break;
            }
        }

        public void WaitUntilDisappear(string errorMessage, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message(errorMessage)
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => !this.DisplayedNow);
        }

        public bool IsDisappeared(TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .IgnoreExceptionTypes(typeof(WebDriverTimeoutException))
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => !this.DisplayedNow);
        }

        /// <summary>
        /// Wait for the element
        /// </summary>
        /// <param name="errorMessage">Exception contain this message</param>
        /// <param name="timeout">Exnted timeout. By default the timeout is 5s</param>
        /// <exception cref="WebDriverTimeoutException"></exception>
        public void WaitUntilDisplay(string errorMessage, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message(errorMessage)
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => this.DisplayedNow);
        }

        public void ExecuteScript(string script)
        {
            ((IJavaScriptExecutor)this.webDriver).ExecuteScript(script, this.Finder.WebElement());
        }


        protected Color GetColor()
        {
            const byte NOT_TRANSPARENT = 255;
            var color = Color.FromArgb(0, 0, 0, 0);
            this.Do(() =>
            {
                var cssColor = this.Do(() => this.Finder.WebElement().GetCssValue("color"));
                var currentColor = ColorUtil.ParseColor(cssColor);

                if (currentColor.A == NOT_TRANSPARENT)
                {
                    color = currentColor;
                }
                else
                {
                    var backgroundColor = this.GetBackgroundColor();
                    color = ColorUtil.Blend(currentColor, backgroundColor);
                }

            });
            return color;
        }

        protected Color GetBackgroundColor()
        {
            const byte FULLY_TRANSPARENT = 0;
            const byte NOT_TRANSPARENT = 255;
            var color = Color.FromArgb(0, 0, 0, 0);
            this.Do(() =>
            {
                var elementToCheck = this.Finder.WebElement();
                do
                {
                    var e = elementToCheck;
                    var cssColor = this.Do(() => e.GetCssValue("background-color"));
                    var currentColor = ColorUtil.ParseColor(cssColor);

                    if (currentColor.A != FULLY_TRANSPARENT)
                    {
                        color = ColorUtil.Blend(color, currentColor);
                    }

                    elementToCheck = elementToCheck.FindElement(By.XPath(".."));

                } while (color.A != NOT_TRANSPARENT);
            });
            return color;
        }

        private T Do<T>(Func<T> action) => Retry.Do<StaleElementReferenceException, T>(action);

        protected void Do(Action action)
        {
            Retry.Do<StaleElementReferenceException>(action);
        }

        public void DragAndDrop(int xAxis, int yAxis)
        {
            new Actions(this.webDriver)
                .DragAndDropToOffset(this.Finder.WebElement(), offsetX: xAxis, offsetY: yAxis)
                .Build()
                .Perform();
        }

        public void Swipe(int xAxis)
        {
            new Actions(this.webDriver)
                .DragAndDropToOffset(this.Finder.WebElement(), offsetX: this.Location.X + xAxis, offsetY: this.Location.Y)
                .Perform();

            Thread.Sleep(500);
        }

        public void Scroll()
        {
            try
            {
                new Actions(this.webDriver)
                     .MoveToElement(this.Finder.WebElement())
                     .Perform();
            }
            catch (MoveTargetOutOfBoundsException)
            {
                this.ExecuteScript("arguments[0].scrollIntoView(true);");
            }
        }

        public string GetComputedStyle(string style, string pseudo = null)
        {
            var pseudoText = pseudo != null
                ? $", '{pseudo}'"
                : string.Empty;
            var script = $"return window.getComputedStyle(arguments[0] {pseudoText}).getPropertyValue('{style}')";
            return this.Do(() =>
                ((IJavaScriptExecutor)this.webDriver).ExecuteScript(script, this.Finder.WebElement()).ToString());
        }

        public string GetCssValue(string propertyName)
        {
            return this.Do(() => this.Finder.WebElement().GetCssValue(propertyName));
        }
    }
}
