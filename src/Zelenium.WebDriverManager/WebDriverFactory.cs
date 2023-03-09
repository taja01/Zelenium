using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Zelenium.Core.Enums;
using Zelenium.WebDriverManager.Options;

namespace Zelenium.WebDriverManager
{
    public class WebDriverFactory
    {
        private IWebDriver driver;
        public IWebDriver GetWebDriver(Browser browser, string remoteUrl, bool runInHeadlessMode = true)
        {
            return this.GetWebDriver(browser, Device.Desktop, remoteUrl, runInHeadlessMode);
        }

        public IWebDriver GetWebDriver(Browser browser, Device device, string remoteUrl, bool runInHeadlessMode = true)
        {
            if (this.driver != null)
            {
                return this.driver;
            }

            try
            {
                DriverOptions options = null;
                options = browser switch
                {
                    Browser.Chrome => this.GetChromeOptions(device, runInHeadlessMode, false),
                    Browser.Firefox => this.GetFirefoxOptions(runInHeadlessMode),
                    Browser.Edge => this.GetEdgeOptions(device, runInHeadlessMode),
                    _ => throw new NotImplementedException()
                };

                this.driver = new RemoteWebDriver(new Uri(remoteUrl), options);

                return this.driver;

            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        public IWebDriver GetWebDriver(Browser browser, bool runInHeadlessMode, bool useModHeader, string path = null)
        {
            return this.GetWebDriver(browser, Device.Desktop, runInHeadlessMode, useModHeader, path);
        }

        public IWebDriver GetWebDriver(Browser browser, Device device, bool runInHeadlessMode = true, bool useModHeader = false, string path = null)
        {
            if (this.driver != null)
            {
                return this.driver;
            }

            try
            {
                this.driver = browser switch
                {
                    Browser.Chrome => this.CreateChromeDriver(device, runInHeadlessMode, useModHeader),
                    Browser.Firefox => this.CreateFirefoxDriver(runInHeadlessMode, useModHeader),
                    Browser.Edge => this.CreateEdgeDriver(device, runInHeadlessMode, path),
                    _ => throw new NotImplementedException()
                };

                return this.driver;

            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        private IWebDriver CreateChromeDriver(Device device, bool runInHeadlessMode, bool useModHeader)
        {
            return new ChromeDriver((ChromeOptions)this.GetChromeOptions(device, runInHeadlessMode, useModHeader));
        }

        private DriverOptions GetChromeOptions(Device device, bool runInHeadlessMode, bool useModHeader)
        {
            if (runInHeadlessMode && useModHeader)
            {
                throw new Exception($"Chrome cannot use ModHeader in headless mod!\n" +
                    $"{nameof(runInHeadlessMode)}: {runInHeadlessMode}\n" +
                    $"{nameof(useModHeader)}: {useModHeader}");
            }

            return ChromeOptionsDirector
                .NewChromeOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(runInHeadlessMode)
                .WithExtension(useModHeader)
                .Build();
        }

        private IWebDriver CreateFirefoxDriver(bool runInHeadlessMode, bool useModHeader)
        {
            var driver = new FirefoxDriver((FirefoxOptions)this.GetFirefoxOptions(runInHeadlessMode));

            if (useModHeader)
            {
                driver.InstallAddOnFromFile($@"{Path.GetFullPath(@"BrowserExtensions")}\modheader_ff.xpi");
            }

            return driver;
        }

        private DriverOptions GetFirefoxOptions(bool runInHeadlessMode)
        {
            return FireFoxOptionsDirector
                .NewFirefoxOptionsDirector
                .SetCommon()
                .SetHeadless(runInHeadlessMode)
                .Build();
        }

        private IWebDriver CreateEdgeDriver(Device device, bool debug = true, string path = null)
        {
            return new EdgeDriver(path, (EdgeOptions)this.GetEdgeOptions(device, debug));
        }

        private DriverOptions GetEdgeOptions(Device device, bool debug = true)
        {
            return EdgeOptionsDirector
                .NewEdgeOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
                .Build();
        }
    }
}
