using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class VisitManagerException : Exception
    {
        public VisitManagerException()
        {
        }

        public VisitManagerException(string? message) : base(message)
        {
        }

        public VisitManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected VisitManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
