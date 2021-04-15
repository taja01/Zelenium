using System.IO;
using OpenQA.Selenium.Chrome;
using Zelenium.Shared;

namespace Zelenium.WebDriverManager.Options
{
    public abstract class ChromeOptionsBuilder
    {
        protected readonly ChromeOptions chromeOptions;

        protected ChromeOptionsBuilder()
        {
            this.chromeOptions = new ChromeOptions();
        }

        public ChromeOptions Build() => this.chromeOptions;
    }

    public class ChromeCommonSettingsBuilder<T> : ChromeOptionsBuilder where T : ChromeCommonSettingsBuilder<T>
    {
        public T SetCommon()
        {
            this.chromeOptions.AddArgument("--no-sandbox");
            this.chromeOptions.AddArgument("--allow-running-insecure-content");
            this.chromeOptions.AddArgument("--ignore-gpu-blocklis");
            this.chromeOptions.AcceptInsecureCertificates = true;

            // Disable automation info-bar message
            this.chromeOptions.AddExcludedArgument("enable-automation");
            // Disable pop up 'Disable developer mode extensions'
            this.chromeOptions.AddAdditionalOption("useAutomationExtension", false);

            // Disable chrome save your password pop up
            this.chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            this.chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            this.chromeOptions.AddArgument("disable-blink-features=AutomationControlled");

            return (T)this;
        }
    }

    public class ChromeDeviceSettingsBuilder<T> : ChromeCommonSettingsBuilder<ChromeDeviceSettingsBuilder<T>> where T : ChromeDeviceSettingsBuilder<T>
    {
        public T SetDevice(Device device)
        {
            if (device == Device.Desktop)
            {
                this.chromeOptions.AddArgument("--window-size=1920,1080");
            }
            else
            {
                var chromeMobile = new OpenQA.Selenium.Chromium.ChromiumMobileEmulationDeviceSettings
                {
                    //UserAgent = UserAgentProvider.Get(),
                    PixelRatio = 3.0,
                    Width = 360,
                    Height = 640,
                    EnableTouchEvents = true
                };

                this.chromeOptions.EnableMobileEmulation(chromeMobile);
            }

            return (T)this;
        }
    }

    public class ChromeHeadlessSettingsBuilder<T> : ChromeDeviceSettingsBuilder<ChromeHeadlessSettingsBuilder<T>> where T : ChromeHeadlessSettingsBuilder<T>
    {
        public T SetHeadless(bool debug)
        {
            if (!debug)
            {
                this.chromeOptions.AddArgument("--headless");
            }

            return (T)this;
        }
    }

    public class ChromeExtensionSettingsBuilder<T> : ChromeHeadlessSettingsBuilder<ChromeExtensionSettingsBuilder<T>> where T : ChromeExtensionSettingsBuilder<T>
    {
        public T WithExtension(bool useExtension)
        {
            if (useExtension)
            {
                this.chromeOptions.AddExtension($@"{Path.GetFullPath(@"Driver\ChromeExtensions")}\modheader_2_4_0_0.crx");
            }

            return (T)this;
        }
    }
}
