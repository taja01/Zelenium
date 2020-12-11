using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using ZeleniumFramework.Driver.Options;
using ZeleniumFramework.Enums;

namespace ZeleniumFramework.Driver.Drivers
{
    public class WebDriverFactory
    {
        private IWebDriver driver;

        public IWebDriver GetWebDriver()
        {
            if (this.driver != null)
            {
                return this.driver;
            }

            try
            {
                this.driver = this.CreateChromeDriver(Device.Desktop);
                return this.driver;
            }
            catch (Exception exception)
            {
                throw new SystemException("Failed to initialize WebDriver:" + exception.Message, exception);
            }
        }

        private IWebDriver CreateChromeDriver(Device device)
        {

            var options = ChromeOptionsDirector.NewChromeOptionsDirector.
                SetCommon().
                SetDevice(device).
                Build();

            return new ChromeDriver(options);
        }
    }
}
