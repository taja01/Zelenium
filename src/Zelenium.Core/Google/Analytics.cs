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

        /// <summary>
        /// Clear data layer. Useful before event trigger to find your events
        /// </summary>
        public void ClearDataLayer() => this.executor.Execute(AnalyticsQueries.ClearDataLayer.Script);

        /// <summary>
        /// Return with DataLayer as object
        /// </summary>
        /// <returns>object</returns>
        public object GetDataLayer() => this.executor.Execute(AnalyticsQueries.GetDataLayer.Script);

    }
}
