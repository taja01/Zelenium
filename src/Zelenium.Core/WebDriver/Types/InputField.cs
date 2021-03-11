using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.Core.WebDriver.Types
{
    public class InputField : Element
    {
        public InputField(IWebDriver webDriver, By locator = null) : base(webDriver, locator)
        {
        }


        public override string Text => this.Attributes.Get("value");
        public string Placeholder => this.Attributes.Get("placeholder");

        public virtual void Clear()
        {
            this.Finder.GetDisplayedWebElement().Clear();
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

        public virtual void SendKeys(string text)
        {
            Wait.Initialize()
                .Timeout(TimeConfig.LongTimeout)
                .Message($"Couldn't set text \"{text}\" for input element {this.Path}")
                .IgnoreExceptionTypes(typeof(StaleElementReferenceException))
                .Until(() =>
                {
                    this.Clear();
                    this.Finder.GetWebElement().SendKeys(text);
                    return this.Text == text;
                });
        }

        public virtual void SendKeysSpecial(string text)
        {
            this.Do(() => this.Finder.GetWebElement().SendKeys(text));
        }
    }
}
