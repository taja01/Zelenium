using System;
using System.Drawing;
using System.Threading;
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
        protected AbstractElement(IWebDriver webDriver, By locator = null)
        {
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
        public Point Location => this.Do(() => this.Finder.GetWebElement().Location);
        public bool Displayed => this.Finder.Displayed();
        public bool Present => this.Finder.Present();
        public bool DisplayedNow => this.Finder.Displayed(TimeSpan.Zero);
        public bool PresentNow => this.Finder.Present(TimeSpan.Zero);
        public Color BackgroundColor => this.GetBackgroundColor();
        public ClassAttribute Class => new ClassAttribute(this.Finder);
        public Attributes Attributes => new Attributes(this.Finder);
        public string Path => this.Finder.Path;

        public JavaScriptExecutor JavaScriptExecutor { get; private set; }

        public void Click(ClickMethod clickMethod = ClickMethod.Default)
        {
            switch (clickMethod)
            {
                case ClickMethod.Default:
                    {
                        try
                        {
                            this.Scroll();
                            this.WebElement.Click();
                        }
                        catch (ElementClickInterceptedException e)
                        {
                            Console.WriteLine($"Failed to 'click' emelent: {this.Path}");
                            Console.WriteLine("Trying to click using javascript!");
                            Console.WriteLine(e.Message);
                            this.ExecuteScript(BaseQueries.JavaScriptClick);
                        }

                        break;
                    }
                case ClickMethod.Javascript: this.ExecuteScript(BaseQueries.JavaScriptClick); break;
                case ClickMethod.NewTab: this.ExecuteScript(BaseQueries.OpenLinkInNewTab); break;
            }
        }

        public void WaitUntilDisappear(string errorMessage, TimeSpan? timeout = null)
        {
            Wait.Initialize()
                .Message($"Element '{errorMessage}' is still visible")
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
                .Message($"Element '{errorMessage}' does not appear")
                .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                .Until(() => this.DisplayedNow);
        }

        public void ExecuteScript(JsQuery script)
        {
            this.JavaScriptExecutor.Execute(script.Script, this);
        }

        public void ExecuteScript(JsQuery script, out object result)
        {
            result = this.JavaScriptExecutor.Execute(script.Script, this);
        }

        public void ExecuteScript<T>(JsQuery script, out T result)
        {
            result = this.JavaScriptExecutor.Get<T>(script, this);
        }

        protected Color GetColor()
        {
            const byte NOT_TRANSPARENT = 255;
            var color = Color.FromArgb(0, 0, 0, 0);
            this.Do(() =>
            {
                var cssColor = this.Do(() => this.Finder.GetDisplayedWebElement().GetCssValue("color"));
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
                var elementToCheck = this.Finder.GetDisplayedWebElement();
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
                     .MoveToElement(this.Finder.GetDisplayedWebElement())
                     .Perform();
            }
            catch (MoveTargetOutOfBoundsException)
            {
                this.ExecuteScript(BaseQueries.ScrollToView);
            }
        }

        public string GetComputedStyle(string style, string pseudo = null)
        {
            return this.Do(() => this.JavaScriptExecutor.Get<string>(BaseQueries.GetComputedStyle(style, pseudo), this).ToString());
        }

        public string GetCssValue(string propertyName)
        {
            return this.Do(() => this.Finder.GetDisplayedWebElement().GetCssValue(propertyName));
        }
    }
}
