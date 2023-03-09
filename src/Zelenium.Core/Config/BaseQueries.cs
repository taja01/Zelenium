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

        public static JsQuery GetInnerText => new JsQuery
        {
            Name = "GetInnerText",
            Script = "return arguments[0].innerText;"
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

        public static JsQuery GetShadowElements(string cssSelector) => new JsQuery
        {
            Name = "GetShadowElements",
            Script = $"return arguments[0].shadowRoot.querySelectorAll('{cssSelector}')"
        };

        public static JsQuery SetStyle(string value) => new JsQuery
        {
            Name = "SetStyle",
            Script = $"arguments[0].setAttribute('Style', '{value}')"
        };

        public static JsQuery GetStyle() => new JsQuery
        {
            Name = "GetStyle",
            Script = $"return arguments[0].getAttribute('Style')"
        };

        public static JsQuery AddAttribute(string attribute, string value) => new JsQuery
        {
            Name = "AddAttribute",
            Script = $"arguments[0].setAttribute('{attribute}', '{value}')"
        };

        public static JsQuery GetAttribute(string attribute) => new JsQuery
        {
            Name = "AddAttribute",
            Script = $"return arguments[0].getAttribute('{attribute}')"
        };

        public static JsQuery IsInView() => new JsQuery
        {
            Name = "IsInView",
            Script = "var elem = arguments[0], box = elem.getBoundingClientRect(), cx = box.left + box.width / 2,  cy = box.top + box.height / 2,  e =document.elementFromPoint(cx, cy); for (; e; e = e.parentElement) { if (e === elem) return true;} return false;"
        };

        static string SetPseudoText(string pseudo)
        {
            return pseudo != null ? $", '{pseudo}'" : string.Empty;
        }
    }
}
