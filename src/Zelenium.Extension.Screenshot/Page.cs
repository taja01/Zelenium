using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;

namespace Zelenium.Extension.Screenshot
{
    public class Page(IWebDriver driver)
    {
        private readonly IWebDriver _driver = driver;

        public int Width
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>("return document.body.scrollWidth").ToString();
                return int.Parse(rawValue);
            }
        }

        public int Height
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>("return document.body.scrollHeight").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ScrollBarX
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>("return window.scrollX").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ScrollBarY
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>("return window.scrollY").ToString();
                return (int)Convert.ToDouble(rawValue);
            }
        }

        public void ScrollTo(int x, int y)
        {
            this._driver.ExecuteJavaScript($"window.scrollTo({x}, {y});");
        }

        public void ScrollTo(int y)
        {
            this._driver.ExecuteJavaScript($"window.scrollTo(0, {y});");
        }

        public int ViewWidth
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>($"return window.innerWidth").ToString();
                return int.Parse(rawValue);
            }
        }

        public int ViewHeight
        {
            get
            {
                var rawValue = this._driver.ExecuteJavaScript<object>($"return window.innerHeight").ToString();
                return int.Parse(rawValue);
            }
        }
#pragma warning disable CA1416 // Validate platform compatibility
        public void GetFullScreenShot(string fileName)
        {
            //Ideal world would be if IWebDriver has GetFullPageScreenshot method...
            if (this._driver is FirefoxDriver firefoxDriver)
            {
                var screenshot = firefoxDriver.GetFullPageScreenshot();
                screenshot.SaveAsFile(fileName);
            }
            else
            {
                try
                {
                    this.ScrollTo(0, 0);
                    var pageHeight = this.Height;
                    var windowHeight = this.ViewHeight;

                    if (windowHeight < pageHeight)
                    {
                        var imageList = this.GetScreenShots(pageHeight, windowHeight);

                        var images = imageList.Select(s => ConvertToImage(s.AsByteArray));

                        var currentImage = images.First();

                        var fullScreenshotImage = new Bitmap(currentImage.Width, pageHeight);

                        var windowHeightActualPosition = 0;
                        using (var g = Graphics.FromImage(fullScreenshotImage))
                        {
                            for (var i = 0; i < images.Count(); i++)
                            {
                                currentImage = images.ElementAt(i);

                                if (i == imageList.Count - 1)
                                {
                                    windowHeightActualPosition -= windowHeight - (pageHeight - pageHeight / windowHeight * windowHeight);
                                    g.DrawImage(currentImage, 0, windowHeightActualPosition);
                                }
                                else
                                {
                                    g.DrawImage(currentImage, 0, windowHeightActualPosition);
                                    windowHeightActualPosition += windowHeight;
                                }
                            }
                        }

                        fullScreenshotImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else
                    {
                        this.CreateScreenShot().SaveAsFile(fileName);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create Screenshot with Extension", ex);
                }
            }
        }

        private List<OpenQA.Selenium.Screenshot> GetScreenShots(int pageHeight, int windowHeight)
        {
            var fullJump = pageHeight / windowHeight;
            var screenshotList = new List<OpenQA.Selenium.Screenshot>();
            for (var i = 0; i < fullJump; i++)
            {
                screenshotList.Add(this.CreateScreenShot());
                this.ScrollTo(this.ScrollBarY + windowHeight);
            }

            screenshotList.Add(this.CreateScreenShot());

            return screenshotList;
        }

        private OpenQA.Selenium.Screenshot CreateScreenShot()
        {
            var screenshot = ((ITakesScreenshot)this._driver).GetScreenshot();

#if DEBUG
            screenshot.SaveAsFile($"{Environment.CurrentManagedThreadId + 10}_{DateTime.Now.ToFileTime()}.png");
#endif
            return screenshot;
        }

        private static Image ConvertToImage(byte[] byteArrayIn)
        {
            using var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}
#pragma warning restore CA1416 // Validate platform compatibility
