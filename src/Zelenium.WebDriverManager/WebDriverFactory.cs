using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Zelenium.Shared;
using Zelenium.WebDriverManager.Options;

namespace Zelenium.WebDriverManager
{
    public class WebDriverFactory
    {
        private IWebDriver driver;
        public IWebDriver GetWebDriver(Browser browser, string remoteUrl, bool debug = true)
        {
            return this.GetWebDriver(browser, Device.Desktop, remoteUrl, debug);
        }
        public IWebDriver GetWebDriver(Browser browser, Device device, string remoteUrl, bool debug = true)
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
                    Browser.Chrome => this.GetChromeOptions(device, debug),
                    Browser.Firefox => this.GetFirefoxOptions(device, debug),
                    Browser.Edge => this.GetEdgeOptions(device, debug),
                    _ => throw new NotImplementedException()
                };
                return new RemoteWebDriver(new Uri(remoteUrl), options);

            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        public IWebDriver GetWebDriver(Browser browser, bool debug = true, string path = null)
        {
            return this.GetWebDriver(browser, Device.Desktop, debug, path);
        }

        public IWebDriver GetWebDriver(Browser browser, Device device, bool debug = true, string path = null)
        {
            if (this.driver != null)
            {
                return this.driver;
            }

            try
            {
                return browser switch
                {
                    Browser.Chrome => this.CreateChromeDriver(device, debug),
                    Browser.Firefox => this.CreateFirefoxDriver(device, debug),
                    Browser.Edge => this.CreateEdgeDriver(device, debug, path),
                    _ => throw new NotImplementedException()
                };

            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        private IWebDriver CreateChromeDriver(Device device, bool debug = true)
        {
            return new ChromeDriver((ChromeOptions)this.GetChromeOptions(device, debug));
        }

        private DriverOptions GetChromeOptions(Device device, bool debug = true)
        {
            return ChromeOptionsDirector
                .NewChromeOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
                .Build();
        }

        private IWebDriver CreateFirefoxDriver(Device device, bool debug = true)
        {
            var geckoService = FirefoxDriverService.CreateDefaultService();
            geckoService.Host = "::1";

            return new FirefoxDriver(geckoService, (FirefoxOptions)this.GetFirefoxOptions(device, debug));
        }

        private DriverOptions GetFirefoxOptions(Device device, bool debug = true)
        {
            return FireFoxOptionsDirector
                .NewFirefoxOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
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
