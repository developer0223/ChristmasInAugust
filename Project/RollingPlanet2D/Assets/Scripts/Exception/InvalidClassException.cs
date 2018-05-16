using System;

namespace MyException
{
    public class InvalidClassException : Exception
    {
        public InvalidClassException() : base() { }
        public InvalidClassException(string message) : base(message) { }
    }
}