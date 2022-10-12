using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class VisitException : Exception
    {
        public VisitException(string? message) : base(message)
        {
        }

        public VisitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
