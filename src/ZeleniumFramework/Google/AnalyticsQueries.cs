using ZeleniumFramework.Helper;

namespace ZeleniumFramework.Google
{
    public static class AnalyticsQueries
    {
        public static JsQuery GetGoogleAnalyticsID => new JsQuery
        {
            Name = "GetGoogleAnalyticsID",
            Script = "return ga.getAll()[0].get('trackingId')"
        };
    }
}
