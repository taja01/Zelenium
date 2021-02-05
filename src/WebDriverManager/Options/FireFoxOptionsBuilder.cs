using System;
using OpenQA.Selenium.Firefox;

namespace WebDriverManager.Options
{
    internal abstract class FireFoxOptionsBuilder
    {
        protected readonly FirefoxOptions firefoxOptions;

        public FireFoxOptionsBuilder()
        {
            this.firefoxOptions = new FirefoxOptions();
        }

        public FirefoxOptions Build() => this.firefoxOptions;
    }

    internal class FirefoxCommonSettingsBuilder<T> : FireFoxOptionsBuilder where T : FirefoxCommonSettingsBuilder<T>
    {
        public T SetCommon()
        {
            this.firefoxOptions.AddArgument("--no-sandbox");
            this.firefoxOptions.AddArgument("--allow-running-insecure-content");
            this.firefoxOptions.AddArgument("--ignore-gpu-blocklis");

            this.firefoxOptions.AcceptInsecureCertificates = true;

            /*
            // Disable automation info-bar message
            this.firefoxOptions.AddExcludedArgument("enable-automation");
            // Disable pop up 'Disable developer mode extensions'
            this.firefoxOptions.AddAdditionalCapability("useAutomationExtension", false);

            // Disable chrome save your password pop up
            this.firefoxOptions.AddUserProfilePreference("credentials_enable_service", false);
            this.firefoxOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            */
            return (T)this;
        }
    }

    internal class FirefoxDeviceSettingsBuilder<T> : FirefoxCommonSettingsBuilder<FirefoxDeviceSettingsBuilder<T>> where T : FirefoxDeviceSettingsBuilder<T>
    {
        public T SetDevice(Device device)
        {
            if (device == Device.Desktop)
            {
                this.firefoxOptions.AddArgument("--window-size=1920,1080");
            }
            else
            {
                throw new NotSupportedException("Firefox does not support mobile");
            }

            return (T)this;
        }
    }

    internal class FirefoxHeadlessSettingsBuilder<T> : FirefoxDeviceSettingsBuilder<FirefoxHeadlessSettingsBuilder<T>> where T : FirefoxHeadlessSettingsBuilder<T>
    {
        public T SetHeadless(bool debug)
        {
            if (!debug)
            {
                this.firefoxOptions.AddArgument("--headless");
            }

            return (T)this;
        }
    }
}
