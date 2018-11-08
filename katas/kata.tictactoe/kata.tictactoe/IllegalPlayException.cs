using System;

namespace Tests
{
    public class IllegalPlayException : Exception
    {
        public IllegalPlayException(string message) : base(message)
        {
        }
    }
}