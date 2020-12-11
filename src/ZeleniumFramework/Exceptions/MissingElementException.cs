using OpenQA.Selenium;
using System;

namespace ZeleniumFramework.Exceptions
{
    [Serializable]
    public class MissingElementException : WebDriverException
    {
        public MissingElementException(string message) : base(message)
        {
        }

        public MissingElementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
