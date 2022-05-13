using System;
using System.Runtime.Serialization;

namespace Services
{
    public class NumberServiceNotFoundException : Exception
    {
        public NumberServiceNotFoundException()
        {
        }

        public NumberServiceNotFoundException(string message) : base(message)
        {
        }
    }
}