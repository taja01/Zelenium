using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ZeleniumFramework.WebDriver
{
    public class Wait
    {
        private static readonly Type DefaultException = typeof(WebDriverTimeoutException);
        private readonly IWait<bool> wait;
        private Type exceptionType = DefaultException;

        public Wait()
        {
            this.wait = new DefaultWait<bool>(true) { Timeout = TimeSpan.FromSeconds(5) };
        }

        public static Wait Initialize()
        {
            return new Wait();
        }

        public Wait Timeout(TimeSpan? timeout)
        {
            if (timeout.HasValue)
            {
                this.wait.Timeout = timeout.Value;
            }

            return this;
        }

        public Wait Message(string message)
        {
            this.wait.Message = message;
            return this;
        }

        public Wait PollingInterval(TimeSpan pollingInterval)
        {
            this.wait.PollingInterval = pollingInterval;
            return this;
        }

        public Wait IgnoreExceptionTypes(params Type[] exceptionTypes)
        {
            this.wait.IgnoreExceptionTypes(exceptionTypes);
            return this;
        }

        public Wait Throw<T>() where T : Exception
        {
            this.exceptionType = typeof(T);
            return this;
        }

        public bool Success(Func<bool> condition)
        {
            try
            {
                this.Until(condition);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void Until(Func<bool> condition)
        {
            bool Func(bool b) => condition();
            try
            {
                _ = this.wait.Until(Func);
            }
            catch (WebDriverTimeoutException e) when (this.exceptionType != DefaultException)
            {
                throw (Exception)Activator.CreateInstance(this.exceptionType, e.Message);
            }
        }

        public void Until<T>(Func<T> func, Func<T, bool> success, Func<T, string> message)
        {
            T t = default;
            try
            {
                _ = this.wait.Until(_ =>
                  {
                      t = func();
                      return success(t);
                  });
            }
            catch (WebDriverTimeoutException e) when (this.exceptionType == DefaultException)
            {
                throw (Exception)Activator.CreateInstance(this.exceptionType, $"{e.Message} | {message(t)}");
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        public TResult Until<TResult>(Func<TResult> condition)
        {
            TResult Func(bool b) => condition();
            try
            {
                return this.wait.Until(Func);
            }
            catch (WebDriverTimeoutException e) when (this.exceptionType != DefaultException)
            {
                throw (Exception)Activator.CreateInstance(this.exceptionType, e.Message);
            }
        }
    }
}
