using System;
using System.Runtime.Serialization;

namespace Services
{
    public class NumberServiceException : Exception
    {
        public NumberServiceException()
        {
        }

        public NumberServiceException(string message) : base(message)
        {
        }
    }
}