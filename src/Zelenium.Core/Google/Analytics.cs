using OpenQA.Selenium;
using Zelenium.Core.Utils;

namespace Zelenium.Core.Google
{
    public class Analytics
    {
        private readonly JavaScriptExecutor executor;

        public Analytics(IWebDriver driver)
        {
            this.executor = new JavaScriptExecutor(driver);
        }

        /// <summary>
        /// Get Google tracking id 
        /// </summary>
        public string GoogleAnalyticsID => this.executor.Get<string>(AnalyticsQueries.GetGoogleAnalyticsID);
    }
}
