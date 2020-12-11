using System;

namespace ZeleniumFramework.Exceptions
{
    [Serializable]
    public class TestException : Exception
    {
        public TestException(string message) : base(message)
        {
        }
    }

}