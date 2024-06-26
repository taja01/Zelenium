﻿using System;

namespace Zelenium.Core.Exceptions
{
    [Serializable]
    public class AttributeNotExistException : Exception
    {
        public AttributeNotExistException()
        {
        }

        public AttributeNotExistException(string message) : base(message)
        {
        }

        public AttributeNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
