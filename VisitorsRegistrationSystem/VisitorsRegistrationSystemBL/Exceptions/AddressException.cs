using System.Runtime.Serialization;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class AddressException : Exception
    {
        public AddressException()
        {
        }

        public AddressException(string? message) : base(message)
        {
        }

        public AddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}