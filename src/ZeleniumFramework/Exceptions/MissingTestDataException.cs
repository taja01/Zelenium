using System;
using NUnit.Framework;

namespace ZeleniumFramework.Exceptions
{
    [Serializable]
    public class MissingTestDataException : IgnoreException
    {
        public MissingTestDataException(string message) : base(message)
        {
        }
    }
}
