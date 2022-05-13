using System;
using System.Runtime.Serialization;

namespace Logic
{
    public class InvalidProductDataException : Exception
    {
        public InvalidProductDataException()
        {
        }

        public InvalidProductDataException(string message) : base(message)
        {
        }
    }
}