using System;
using System.Drawing;
using System.Threading;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Zelenium.Core.Config;
using Zelenium.Core.Enums;
using Zelenium.Core.Helper;
using Zelenium.Core.Interfaces;
using Zelenium.Core.Utils;

namespace Zelenium.Core.WebDriver.Types
{
    public abstract class AbstractElement : IElementContainer
    {
        protected readonly IWebDriver webDriver;
        protected readonly ILogger logger;
        protected AbstractElement(ILogger logger, IWebDriver webDriver, By locator = null)
        {
            this.logger = logger;
            this.webDriver = webDriver;
            if (locator != null)
            {
                this.Finder = new ElementFinder(this.webDriver, null, locator);
                this.JavaScriptExecutor = new JavaScriptExecutor(this.webDriver);
            }
        }

        public IElementFinder Finder { get; set; }
        public IWebElement WebElement => this.Finder.GetWebElement();
        public IWebElement DisplayedWebElement => this.Finder.GetDisplayedWebElement();
        public Point Location => Do(() => this.Finder.GetWebElement().Location);
        public bool Displayed => Do(() => this.Finder.Displayed());
        public bool Present => this.Finder.Present();
        public bool DisplayedNow => Do(() => this.Finder.Displayed(TimeSpan.Zero));
        public bool PresentNow => this.Finder.Present(TimeSpan.Zero);
        public Color BackgroundColor => this.GetBackgroundColor();
        public Color BorderColor => this.GetBorderColor();
        public ClassAttribute Class => new(this.Finder);
        public Attributes Attributes => new(this.Finder);
        public string Path => this.Finder.Path;
        public bool Selected => Do(() => this.Finder.GetWebElement().Selected);
        public bool Enabled => Do(() => this.Finder.GetWebElement().Enabled);
        public JavaScriptExecutor JavaScriptExecutor { get; private set; }
        public bool HasBeforePseudo => this.GetComputedStyle("content", ":before") != "none";
        public bool HasAfterPseudo => this.GetComputedStyle("content", ":after") != "none";
        public void Click(ClickMethod clickMethod = ClickMethod.Default)
        {
            switch (clickMethod)
            {
                case ClickMethod.Default:
                    {
                        try
                        {
                            Do(() =>
                            {
                                this.Scroll();
                                this.WebElement.Click();
                            });
                        }
                        catch (ElementClickInterceptedException e)
                        {
                            this.logger.LogError(e, "Failed to 'click' element: {path}", this.Path);
                            this.logger.LogWarning("Trying to click using javascript!");

                            this.ExecuteScript(BaseQueries.JavaScriptClick);
                        }

                        break;
                    }
                case ClickMethod.JavaScript: this.ExecuteScript(BaseQueries.JavaScriptClick); break;
                case ClickMethod.NewTab: this.ExecuteScript(BaseQueries.OpenLinkInNewTab); break;
            }
        }

        public void WaitUntilDisappear(string elementName, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message(GenerateErrorMessage(this, elementName, "Element still visible"))
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

        private static string GenerateErrorMessage(IElementContainer element, string elementName, string mainReason)
        {
            return $"{mainReason}{Environment.NewLine}" +
                            $"Type: {element.GetType().Name}{Environment.NewLine}" +
                            $"Property name: {elementName}{Environment.NewLine}" +
                            $"Path: {element.Path}";
        }

        /// <summary>
        /// Wait for the element
        /// </summary>
        /// <param name="elementName">Name of the element</param>
        /// <param name="timeout">Extend timeout. By default the timeout is 5s</param>
        /// <exception cref="WebDriverTimeoutException"></exception>
        public void WaitUntilDisplay(string elementName, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message(GenerateErrorMessage(this, elementName, "Element still not visible"))
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => this.DisplayedNow);
        }

        public void ExecuteScript(JsQuery script)
        {
            this.JavaScriptExecutor.Execute(script.Script, this);
        }

        public T ExecuteScript<T>(JsQuery script)
        {
            return this.JavaScriptExecutor.Get<T>(script, this);
        }

        protected Color GetColor()
        {
            const byte NOT_TRANSPARENT = 255;
            var color = Color.FromArgb(0, 0, 0, 0);

            this.Scroll();
            Do(() =>
            {
                var cssColor = Do(() => this.Finder.GetDisplayedWebElement().GetCssValue(ColorNames.COLOR));
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

            this.Scroll();
            Do(() =>
            {
                var elementToCheck = this.Finder.GetDisplayedWebElement();
                do
                {
                    var e = elementToCheck;
                    var cssColor = Do(() => e.GetCssValue(ColorNames.BACKGROUND));
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

        private static T Do<T>(Func<T> action) => Retry.Do<StaleElementReferenceException, T>(action, delayBetweenTries: TimeConfig.TinyTimeout);

        protected static void Do(Action action)
        {
            Retry.Do<StaleElementReferenceException>(action, delayBetweenTries: TimeConfig.TinyTimeout);
        }

        public void DragAndDrop(int xAxis, int yAxis)
        {
            new Actions(this.webDriver)
                .DragAndDropToOffset(this.Finder.GetDisplayedWebElement(), offsetX: xAxis, offsetY: yAxis)
                .Build()
                .Perform();
        }

        public void Swipe(int xAxis)
        {
            new Actions(this.webDriver)
                .DragAndDropToOffset(this.Finder.GetDisplayedWebElement(), offsetX: this.Location.X + xAxis, offsetY: this.Location.Y)
                .Perform();

            Thread.Sleep(500);
        }

        public void Scroll()
        {
            try
            {
                new Actions(this.webDriver)
                     .MoveToElement(this.Finder.GetWebElement())
                     .Perform();
            }
            catch (MoveTargetOutOfBoundsException)
            {
                this.ExecuteScript(BaseQueries.ScrollToView);
            }
        }

        public string GetComputedStyle(string style, string pseudo = null)
        {
            return Do(() => this.JavaScriptExecutor.Get<string>(BaseQueries.GetComputedStyle(style, pseudo), this));
        }

        public string GetCssValue(string propertyName)
        {
            return Do(() => this.Finder.GetWebElement().GetCssValue(propertyName));
        }

        public Color GetColor(string propertyName)
        {
            var cssColor = Do(() => this.Finder.GetWebElement().GetCssValue(propertyName));
            return ColorUtil.ParseColor(cssColor);
        }

        public bool IsInViewPort()
        {
            return this.ExecuteScript<bool>(BaseQueries.IsInView());
        }

        public bool IsInViewPortWithin(TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Success(() => this.IsInViewPort());
        }

        public void WaitUntilInViewPort(string elementName, TimeSpan? timeout = null)
        {
            Wait.Initialize()
               .Message(GenerateErrorMessage(this, elementName, "Element still not in ViewPort"))
               .Timeout(timeout ?? TimeConfig.DefaultTimeout)
               .Until(() => this.IsInViewPort());
        }

        private Color GetBorderColor()
        {
            var top = this.GetColor(ColorNames.BORDER_TOP);
            var right = this.GetColor(ColorNames.BORDER_RIGHT);
            var bottom = this.GetColor(ColorNames.BORDER_BOTTOM);
            var left = this.GetColor(ColorNames.BORDER_RIGHT);

            if (top == right && right == bottom && bottom == left)
            {
                return top;
            }
            else
            {
                var ex = new Exception($"Border colors are different!" +
                    $"\nTop: {top}" +
                    $"\nRight: {right}" +
                    $"\nBottom: {bottom}" +
                    $"\nLeft: {left}");

                this.logger.LogError(ex, "GetBorderColor");
                throw ex;
            }
        }
    }
}
