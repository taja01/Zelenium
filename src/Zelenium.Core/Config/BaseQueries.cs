using Zelenium.Core.Helper;

namespace Zelenium.Core.Config
{
    public static class BaseQueries
    {
        public static JsQuery JavaScriptClick => new JsQuery
        {
            Name = "JavaScriptClick",
            Script = "arguments[0].click();"
        };

        public static JsQuery GetInnerHtml => new JsQuery
        {
            Name = "GetInnerHtml",
            Script = "return arguments[0].innerHTML;"
        };

        public static JsQuery OpenLinkInNewTab => new JsQuery
        {
            Name = "OpenLinkInNewTab",
            Script = "window.open(arguments[0], '_blank')"
        };

        public static JsQuery ScrollToView => new JsQuery
        {
            Name = "ScrollToView",
            Script = "arguments[0].scrollIntoView(true);"
        };

        public static JsQuery GetComputedStyle(string style, string pseudo = null) => new JsQuery
        {
            Name = "GetComputedStyle",
            Script = $"return window.getComputedStyle(arguments[0] {SetPseudoText(pseudo)}).getPropertyValue('{style}')"
        };

        public static JsQuery GetShadowElement(string cssSelector) => new JsQuery
        {
            Name = "GetShadowElement",
            Script = $"return arguments[0].shadowRoot.querySelector('{cssSelector}')"
        };

        static string SetPseudoText(string pseudo)
        {
            return pseudo != null ? $", '{pseudo}'" : string.Empty;
        }
    }
}
