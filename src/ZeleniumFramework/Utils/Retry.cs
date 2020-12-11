using System;
using System.Threading;

namespace ZeleniumFramework.Utils
{
    public static class Retry
    {
        public static void Do<TException>(Action action, int numberOfTries = 2, TimeSpan? delayBetweenTries = null) where TException : Exception
        {
            Do<TException, object>(() =>
                  {
                      action();
                      return null;
                  }, numberOfTries, delayBetweenTries);
        }

        public static TRet Do<TException, TRet>(Func<TRet> action, int numberOfTries = 2, TimeSpan? delayBetweenTries = null) where TException : Exception
        {
            TException lastException = null;
            numberOfTries = numberOfTries < 1 ? 1 : numberOfTries;
            for (var currentTry = 1; currentTry <= numberOfTries; currentTry++)
            {
                try
                {
                    return action();
                }
                catch (TException e)
                {
                    lastException = e;
                }
                Thread.Sleep(delayBetweenTries ?? TimeSpan.Zero);
            }
            if (lastException != null)
            {
                throw lastException;
            }
            throw new Exception("No exception to re-throw");
        }
    }
}
