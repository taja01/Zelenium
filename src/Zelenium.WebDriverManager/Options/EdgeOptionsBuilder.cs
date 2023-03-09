using System.IO;
using OpenQA.Selenium.Edge;
using Zelenium.Core.Enums;

namespace Zelenium.WebDriverManager.Options
{
    public class EdgeOptionsBuilder
    {
        protected readonly EdgeOptions edgeOptions;

        protected EdgeOptionsBuilder()
        {
            this.edgeOptions = new EdgeOptions();
        }

        public EdgeOptions Build() => this.edgeOptions;
    }

    public class EdgeCommonSettingsBuilder<T> : EdgeOptionsBuilder where T : EdgeCommonSettingsBuilder<T>
    {
        public T SetCommon()
        {
            this.edgeOptions.AddArgument("--no-sandbox");
            this.edgeOptions.AddArgument("--allow-running-insecure-content");
            this.edgeOptions.AddArgument("--ignore-gpu-blocklis");
            this.edgeOptions.AcceptInsecureCertificates = true;

            // Disable automation info-bar message
            this.edgeOptions.AddExcludedArgument("enable-automation");
            // Disable pop up 'Disable developer mode extensions'
            this.edgeOptions.AddAdditionalOption("useAutomationExtension", false);

            // Disable chrome save your password pop up
            this.edgeOptions.AddUserProfilePreference("credentials_enable_service", false);
            this.edgeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            this.edgeOptions.AddArgument("disable-blink-features=AutomationControlled");

            return (T)this;
        }
    }

    public class EdgeDeviceSettingsBuilder<T> : EdgeCommonSettingsBuilder<EdgeDeviceSettingsBuilder<T>> where T : EdgeDeviceSettingsBuilder<T>
    {
        public T SetDevice(Device device)
        {
            if (device == Device.Desktop)
            {
                this.edgeOptions.AddArgument("--window-size=1920,1080");
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

                this.edgeOptions.EnableMobileEmulation(chromeMobile);
            }

            return (T)this;
        }
    }

    public class EdgeHeadlessSettingsBuilder<T> : EdgeDeviceSettingsBuilder<EdgeHeadlessSettingsBuilder<T>> where T : EdgeHeadlessSettingsBuilder<T>
    {
        public T SetHeadless(bool runInHeadlessMode)
        {
            if (runInHeadlessMode)
            {
                this.edgeOptions.AddArgument("--headless");
            }

            return (T)this;
        }
    }

    public class EdgeExtensionSettingsBuilder<T> : EdgeHeadlessSettingsBuilder<EdgeExtensionSettingsBuilder<T>> where T : EdgeExtensionSettingsBuilder<T>
    {
        public T WithExtension(bool useExtension)
        {
            if (useExtension)
            {
                this.edgeOptions.AddExtension($@"{Path.GetFullPath(@"BrowserExtensions")}\modheader_2_4_0_0.crx");
            }

            return (T)this;
        }
    }
}
