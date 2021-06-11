using OpenQA.Selenium.Firefox;

namespace Zelenium.WebDriverManager.Options
{
    public abstract class FireFoxOptionsBuilder
    {
        protected readonly FirefoxOptions firefoxOptions;

        public FireFoxOptionsBuilder()
        {
            this.firefoxOptions = new FirefoxOptions();
        }

        public FirefoxOptions Build() => this.firefoxOptions;
    }

    public class FirefoxCommonSettingsBuilder<T> : FireFoxOptionsBuilder where T : FirefoxCommonSettingsBuilder<T>
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

    public class FirefoxHeadlessSettingsBuilder<T> : FirefoxCommonSettingsBuilder<FirefoxHeadlessSettingsBuilder<T>> where T : FirefoxHeadlessSettingsBuilder<T>
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
