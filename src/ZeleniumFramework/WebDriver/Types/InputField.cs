using OpenQA.Selenium;
using ZeleniumFramework.Config;

namespace ZeleniumFramework.WebDriver.Types
{
    public class InputField : AbstractElement
    {
        public InputField(IWebDriver webDriver, By locator = null) : base(webDriver, locator)
        {
        }

        public string Value => this.Attributes.Get("value");
        public string Placeholder => this.Attributes.Get("placeholder");

        public void Clear()
        {
            this.Finder.WebElement().Clear();
            //Wait.Initialize()
            //    .Timeout(TimeConfig.LongTimeout)
            //    .Message($"Couldn't clear text for input element {Path}")
            //    .IgnoreExceptionTypes(typeof(StaleElementReferenceException))
            //    .Until(() =>
            //    {
            //        Finder.WebElement().SendKeys(Keys.Control + "a");
            //        Finder.WebElement().SendKeys(Keys.Delete);
            //        return Value.Length == 0;

            //    });
        }

        public void SendKeys(string text)
        {
            Wait.Initialize()
                .Timeout(TimeConfig.LongTimeout)
                .Message($"Couldn't set text \"{text}\" for input element {this.Path}")
                .IgnoreExceptionTypes(typeof(StaleElementReferenceException))
                .Until(() =>
                {
                    this.Clear();
                    this.Finder.WebElement().SendKeys(text);
                    return this.Value == text;
                });
        }

        public void SendKeysSpecial(string text)
        {
            this.Do(() => this.Finder.WebElement().SendKeys(text));
        }
    }
}
