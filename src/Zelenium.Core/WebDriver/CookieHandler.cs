using System;
using OpenQA.Selenium;
using Zelenium.Core.Config;

namespace Zelenium.Core.WebDriver
{
    public class CookieHandler
    {
        private readonly IWebDriver driver;
        private readonly IJavaScriptExecutor executor;

        public CookieHandler(IWebDriver driver, IJavaScriptExecutor executor)
        {
            this.driver = driver;
            this.executor = executor;
        }

        public void AddCookie(Cookie cookie)
        {
            this.driver.Manage().Cookies.AddCookie(cookie);
        }

        /// <summary>
        /// Delete cookies without name.
        /// Selenium cannot handle cookies without name.
        /// </summary>
        public void FixCookieIssue()
        {
            this.executor.ExecuteScript("document.cookie = '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';");
        }

        public Cookie GetCookie(string name)
        {
            return this.IsCookieAdded(name) ? this.driver.Manage().Cookies.GetCookieNamed(name) : null;
        }

        public bool IsCookieAdded(string cookieName, TimeSpan? timeout = null)
        {
            return Wait.Initialize()
                  .Timeout(timeout ?? TimeConfig.DefaultTimeout)
                  .Success(() => this.driver.Manage().Cookies.GetCookieNamed(cookieName) != null);
        }
    }
}
