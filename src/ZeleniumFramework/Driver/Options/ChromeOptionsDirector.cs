namespace ZeleniumFramework.Driver.Options
{
    internal class ChromeOptionsDirector : ChromeExtensionSettingsBuilder<ChromeOptionsDirector>
    {
        public static ChromeOptionsDirector NewChromeOptionsDirector => new ChromeOptionsDirector();
    }
}
