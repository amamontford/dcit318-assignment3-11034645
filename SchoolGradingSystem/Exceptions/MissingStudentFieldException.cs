using System;

namespace SchoolGradingSystem.Exceptions
{
    public class MissingStudentFieldException : Exception
    {
        public MissingStudentFieldException(string message) : base(message) { }
    }
}
