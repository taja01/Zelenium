using Zelenium.Core.Helper;

namespace Zelenium.Core.Google
{
    public static class AnalyticsQueries
    {
        public static JsQuery GetGoogleAnalyticsID => new()
        {
            Name = "GetGoogleAnalyticsID",
            Script = "return ga.getAll()[0].get('trackingId')"
        };

        public static JsQuery ClearDataLayer => new()
        {
            Name = "ClearDataLayer",
            Script = "window.dataLayer = []"
        };

        public static JsQuery GetDataLayer => new()
        {
            Name = "GetDataLayer",
            Script = "return window.dataLayer"
        };
    }
}
