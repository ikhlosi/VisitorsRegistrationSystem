using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    public class VisitorRepositoryException : Exception
    {
        public VisitorRepositoryException()
        {
        }

        public VisitorRepositoryException(string? message) : base(message)
        {
        }

        public VisitorRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected VisitorRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
