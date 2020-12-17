using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using ZeleniumFramework.Driver.Options;
using ZeleniumFramework.Enums;

namespace ZeleniumFramework.Driver.Drivers
{
    public class WebDriverFactory
    {
        private IWebDriver driver;

        public IWebDriver GetWebDriver(Browser browser, bool debug = true, string path = null)
        {
            if (this.driver != null)
            {
                return this.driver;
            }

            try
            {
                return browser switch
                {
                    Browser.Chrome => this.CreateChromeDriver(Device.Desktop, debug),
                    Browser.Firefox => this.CreateFirefoxDriver(Device.Desktop, debug),
                    Browser.Edge => this.CreateEdgeDriver(Device.Desktop, debug, path),
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

            var options = ChromeOptionsDirector
                .NewChromeOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
                .Build();

            return new ChromeDriver(options);
        }

        private IWebDriver CreateFirefoxDriver(Device device, bool debug = true)
        {
            var geckoService = FirefoxDriverService.CreateDefaultService();
            geckoService.Host = "::1";

            var options = FireFoxOptionsDirector
                .NewFirefoxOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
                .Build();

            return new FirefoxDriver(geckoService, options);
        }

        private IWebDriver CreateEdgeDriver(Device device, bool debug = true, string path = null)
        {
            var options = EdgeOptionsDirector
                .NewEdgeOptionsDirector
                .SetCommon()
                .SetDevice(device)
                .SetHeadless(debug)
                .Build();

            return new EdgeDriver(path, options);
        }
    }
}
