using System.Runtime.Serialization;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class CompanyException : Exception
    {
        public CompanyException()
        {
        }

        public CompanyException(string? message) : base(message)
        {
        }

        public CompanyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}