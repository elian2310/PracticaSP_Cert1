using System;
using System.Runtime.Serialization;

namespace Logic
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}