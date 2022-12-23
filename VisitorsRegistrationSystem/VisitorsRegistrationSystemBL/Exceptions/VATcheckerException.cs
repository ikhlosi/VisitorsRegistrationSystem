using System.Runtime.Serialization;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    [Serializable]
    internal class VATcheckerException : Exception
    {
        public VATcheckerException()
        {
        }

        public VATcheckerException(string? message) : base(message)
        {
        }

        public VATcheckerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected VATcheckerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}