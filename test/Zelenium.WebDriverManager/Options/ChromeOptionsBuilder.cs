using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Chrome;
using Zelenium.Core.Enums;

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
            //// This option allows Chrome to load insecure content (http) on a secure page (https). This is 
            //// generally not recommended as it can make your tests less accurately reflect a real use case.
            this.chromeOptions.AddArgument("--allow-running-insecure-content");

            //// This ignores the GPU software rendering list and forces GPU acceleration to be enabled. This is 
            //// used when you want to test website performance with hardware acceleration enabled, but generally 
            //// does not impact testing as most web content is unaffected by GPU acceleration.
            this.chromeOptions.AddArgument("--ignore-gpu-blocklist");

            //// This allows browsing to websites with invalid SSL certificates. This can be useful for testing 
            //// internal sites with self-signed certificates, for example.
            this.chromeOptions.AcceptInsecureCertificates = true;

            //// Not a standard Chrome argument and does not impact the behavior of the browser during automation, 
            //// can be safely removed.
            this.chromeOptions.AddExcludedArgument("enable-automation");

            //// Disables ChromeDriver's built-in automation extension. This extension helps with automating 
            //// things like form controls, but is not strictly necessary and some prefer to disable it.
            this.chromeOptions.AddAdditionalOption("useAutomationExtension", false);

            //// Disables Chrome's autofill feature. This is useful in tests where autofill may interfere with 
            //// manual form input during tests.
            this.chromeOptions.AddUserProfilePreference("credentials_enable_service", false);

            //// Disables Chrome's built-in password manager. Useful in tests where saved passwords may interfere 
            //// with manual form input during tests.
            this.chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            //// Disables the 'AutomationControlled' attribute from the WebDriver, which makes it difficult for 
            //// websites to detect and block WebDriver controlled browsers.
            this.chromeOptions.AddArgument("disable-blink-features=AutomationControlled");

            //// Prevent from asking for location
            this.chromeOptions.AddUserProfilePreference("prefs", new Dictionary<string, object>
                {
                    { "profile.default_content_setting_values.geolocation", 2 }
                });

            return (T)this;
        }
    }

    public class ChromeDeviceSettingsBuilder<T> : ChromeCommonSettingsBuilder<ChromeDeviceSettingsBuilder<T>> where T : ChromeDeviceSettingsBuilder<T>
    {
        public T SetDevice(Device device)
        {
            if (device == Device.Desktop)
            {
                // This sets the size of the browser window when it's first opened by WebDriver.
                // The dimensions are specified in pixels as width, height. 
                // The example here sets the browser to open at a size of 1920x1080, which is a standard size for Full HD displays.
                // This can be useful in tests where you want to ensure your site or app behaves correctly at specific browser window sizes.
                this.chromeOptions.AddArgument("--window-size=1920,1080");
            }
            else
            {
                // Create an object to hold our mobile emulation settings
                var chromeMobile = new OpenQA.Selenium.Chromium.ChromiumMobileEmulationDeviceSettings
                {
                    // UserAgent is the identification string that browsers provide to websites,
                    // we uncomment this line below to set a custom user agent if necessary
                    // UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1",

                    // PixelRatio is the device's pixel density. This affects how things like CSS media queries behave.
                    PixelRatio = 3.0,

                    // Width is the viewport width in CSS pixels (not device pixels). 
                    Width = 360,

                    // Height is the viewport height in CSS pixels (not device pixels).
                    Height = 640,

                    // This flag tells WebDriver to fire touch-related JS events (like 'touchstart', 'touchmove', etc.)
                    // This should be set to true for testing websites that have touch-specific interactions.
                    EnableTouchEvents = true
                };

                // Passes the settings above to the Chrome driver, which will enable Chrome's built-in mobile emulation
                this.chromeOptions.EnableMobileEmulation(chromeMobile);
            }

            return (T)this;
        }
    }

    public class ChromeHeadlessSettingsBuilder<T> : ChromeDeviceSettingsBuilder<ChromeHeadlessSettingsBuilder<T>> where T : ChromeHeadlessSettingsBuilder<T>
    {
        public T SetHeadless(bool runInHeadlessMode)
        {
            if (runInHeadlessMode)
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
                this.chromeOptions.AddExtension($@"{Path.GetFullPath(@"BrowserExtensions")}\modheader_chome.crx");
            }

            return (T)this;
        }
    }

    public class ChromeRemoteSettingsBuilder<T> : ChromeExtensionSettingsBuilder<ChromeRemoteSettingsBuilder<T>> where T : ChromeRemoteSettingsBuilder<T>
    {
        public T WithRemoteSettings(bool remoteAddressAdded)
        {
            if (remoteAddressAdded)
            {
                // This disables the Chrome sandbox, which is a security measure that prevents code from the web page 
                // from executing on the system. This is sometimes needed in Docker or Linux environments where the 
                // sandbox can't be used, but generally, leave this enabled for security.
                this.chromeOptions.AddArgument("--no-sandbox");
            }

            return (T)this;
        }
    }
}
