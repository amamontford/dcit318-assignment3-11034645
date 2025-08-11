using System;

namespace SchoolGradingSystem.Exceptions
{
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }
}
