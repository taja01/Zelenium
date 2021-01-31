using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using ZeleniumFramework.Helper;
using ZeleniumFramework.WebDriver.Interfaces;
using Wait = ZeleniumFramework.WebDriver.Wait;

namespace ZeleniumFramework.Utils
{
    public class JavaScriptExecutor
    {
        private readonly IJavaScriptExecutor executor;
        public JavaScriptExecutor(IWebDriver driver)
        {
            this.executor = (IJavaScriptExecutor)driver;
        }

        public string Query(JsQuery jsQuery, Func<string, bool> condition = null)
        {
            var result = string.Empty;
            Wait.Initialize()
                .Message($"Value {jsQuery.Name} ({jsQuery.Script}) is null, empty or something else")
                .Timeout(jsQuery.Timeout)
                .Throw<IgnoreException>()
                .IgnoreExceptionTypes(typeof(IgnoreException))
                .Until(() =>
                {
                    result = this.GetStringValue(jsQuery);
                    if (string.IsNullOrEmpty(result))
                    {
                        return false;
                    }
                    return condition == null || condition(result);
                });
            return result;
        }

        public void WaitUntilEqual(JsQuery jsQuery, string value)
        {
            var result = string.Empty;
            Wait.Initialize()
                .Message($"{jsQuery.Name} ({jsQuery.Script}) value: {result} is not equal with {value}")
                .Timeout(jsQuery.Timeout)
                .Throw<IgnoreException>()
                .IgnoreExceptionTypes(typeof(IgnoreException))
                .Until(() =>
                {
                    result = this.GetStringValue(jsQuery);
                    return result.ToLower().Equals(value);
                });
        }

        public string QueryUntilNotZero(JsQuery jsQuery)
        {
            static bool NotZero(string input) => input != "0";
            return this.Query(jsQuery, NotZero);
        }

        public IList<T> GetList<T>(JsQuery jsQuery)
        {
            return JsonHelper.RetrieveJsonObjectList<T>(JsonHelper.SerializeObject(this.GetObject(jsQuery)));
        }

        public T Get<T>(JsQuery jsQuery, IElementContainer obj = null, params Newtonsoft.Json.JsonConverter[] converters)
        {
            if (converters == null)
            {
                return JsonHelper.RetrieveJsonObject<T>(JsonHelper.SerializeObject(this.GetObject(jsQuery, obj)));
            }
            return JsonHelper.RetrieveJsonObject<T>(JsonHelper.SerializeObject(this.GetObject(jsQuery, obj)), converters);
        }

        public object Execute(string script, IElementContainer obj = null)
        {
            try
            {
                return obj == null
                    ? this.executor.ExecuteScript(script)
                    : this.executor.ExecuteScript(script, obj.Finder.WebElement());
            }
            catch (Exception e) when (e.GetType() != typeof(NoSuchElementException))
            {
                throw new Exception(e.Message);
            }
        }

        private string GetStringValue(JsQuery data) => this.Execute(data.Script)?.ToString() ?? string.Empty;

        private object GetObject(JsQuery jsQuery, IElementContainer obj = null)
        {
            return Wait.Initialize()
                .Timeout(jsQuery.Timeout)
                .Message($"Value {jsQuery.Name} is null")
                .Throw<IgnoreException>()
                .IgnoreExceptionTypes(typeof(IgnoreException))
                .Until(() => this.Execute(jsQuery.Script, obj));
        }
    }
}
