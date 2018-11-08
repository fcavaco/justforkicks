using System;

namespace kata.GameOfLife
{
    public class IllegalGridException : Exception
    {
        public IllegalGridException(string message) : base(message)
        {
        }
    }
}