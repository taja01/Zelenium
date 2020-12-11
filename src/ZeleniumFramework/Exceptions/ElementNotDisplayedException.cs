using System;
using OpenQA.Selenium;

namespace ZeleniumFramework.Exceptions
{
    [Serializable]
    public class ElementNotDisplayedException : WebDriverException
    {
        public ElementNotDisplayedException()
        {
        }

        public ElementNotDisplayedException(string message) : base(message)
        {
        }

        public ElementNotDisplayedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
