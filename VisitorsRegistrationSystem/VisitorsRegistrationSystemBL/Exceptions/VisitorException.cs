using System.Runtime.Serialization;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    internal class VisitorException : Exception
    {
        public VisitorException()
        {
        }

        public VisitorException(string? message) : base(message)
        {
        }

        public VisitorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}