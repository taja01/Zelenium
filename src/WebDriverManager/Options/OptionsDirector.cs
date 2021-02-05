namespace WebDriverManager.Options
{
    internal class ChromeOptionsDirector : ChromeExtensionSettingsBuilder<ChromeOptionsDirector>
    {
        public static ChromeOptionsDirector NewChromeOptionsDirector => new ChromeOptionsDirector();
    }

    internal class FireFoxOptionsDirector : FirefoxHeadlessSettingsBuilder<FireFoxOptionsDirector>
    {
        public static FireFoxOptionsDirector NewFirefoxOptionsDirector => new FireFoxOptionsDirector();
    }

    internal class EdgeOptionsDirector : EdgeHeadlessSettingsBuilder<EdgeOptionsDirector>
    {
        public static EdgeOptionsDirector NewEdgeOptionsDirector => new EdgeOptionsDirector();
    }
}
