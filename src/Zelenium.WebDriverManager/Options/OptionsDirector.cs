namespace Zelenium.WebDriverManager.Options
{
    public class ChromeOptionsDirector : ChromeRemoteSettingsBuilder<ChromeOptionsDirector>
    {
        public static ChromeOptionsDirector NewChromeOptionsDirector => new ChromeOptionsDirector();
    }

    public class FireFoxOptionsDirector : FirefoxHeadlessSettingsBuilder<FireFoxOptionsDirector>
    {
        public static FireFoxOptionsDirector NewFirefoxOptionsDirector => new FireFoxOptionsDirector();
    }

    public class EdgeOptionsDirector : EdgeHeadlessSettingsBuilder<EdgeOptionsDirector>
    {
        public static EdgeOptionsDirector NewEdgeOptionsDirector => new EdgeOptionsDirector();
    }
}
