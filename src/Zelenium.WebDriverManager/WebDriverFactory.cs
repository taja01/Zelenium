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

        public static IWebDriver GetWebDriver(Browser browser, Device device, string remoteUrl, bool runInHeadlessMode = true)
        {
            try
            {
                DriverOptions options = null;
                options = browser switch
                {
                    Browser.Chrome => GetChromeOptions(device, runInHeadlessMode, false, true),
                    Browser.Firefox => GetFirefoxOptions(runInHeadlessMode),
                    Browser.Edge => GetEdgeOptions(device, runInHeadlessMode),
                    _ => throw new NotImplementedException()
                };

                return new RemoteWebDriver(new Uri(remoteUrl), options);

            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        public static IWebDriver GetWebDriver(Browser browser, bool runInHeadlessMode, bool useModHeader, string path = null)
        {
            return GetWebDriver(browser, Device.Desktop, runInHeadlessMode, useModHeader, path);
        }

        public static IWebDriver GetWebDriver(Browser browser, Device device, bool runInHeadlessMode = true, bool useModHeader = false, string path = null)
        {
            try
            {
                return browser switch
                {
                    Browser.Chrome => CreateChromeDriver(device, runInHeadlessMode, useModHeader),
                    Browser.Firefox => CreateFirefoxDriver(runInHeadlessMode, useModHeader),
                    Browser.Edge => CreateEdgeDriver(device, runInHeadlessMode, path),
                    _ => throw new NotImplementedException()
                };
            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        private static ChromeDriver CreateChromeDriver(Device device, bool runInHeadlessMode, bool useModHeader, bool remoteUrlAdded = false)
        {
            return new ChromeDriver((ChromeOptions)GetChromeOptions(device, runInHeadlessMode, useModHeader, remoteUrlAdded));
        }

        private static ChromeOptions GetChromeOptions(Device device, bool runInHeadlessMode, bool useModHeader, bool remoteUrlAdded)
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
                .WithRemoteSettings(remoteUrlAdded)
                .Build();
        }

        private static FirefoxDriver CreateFirefoxDriver(bool runInHeadlessMode, bool useModHeader)
        {
            var driver = new FirefoxDriver((FirefoxOptions)GetFirefoxOptions(runInHeadlessMode));

            if (useModHeader)
            {
                driver.InstallAddOnFromFile($@"{Path.GetFullPath(@"BrowserExtensions")}\modheader_ff.xpi");
            }

            return driver;
        }

        private static FirefoxOptions GetFirefoxOptions(bool runInHeadlessMode)
        {
            return FireFoxOptionsDirector
                .NewFirefoxOptionsDirector
                .SetCommon()
                .SetHeadless(runInHeadlessMode)
                .Build();
        }

        private static EdgeDriver CreateEdgeDriver(Device device, bool debug = true, string path = null)
        {
            return new EdgeDriver(path, (EdgeOptions)GetEdgeOptions(device, debug));
        }

        private static EdgeOptions GetEdgeOptions(Device device, bool debug = true)
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
