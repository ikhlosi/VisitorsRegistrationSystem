using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class VisitorException : Exception
    {
        public VisitorException(string? message) : base(message)
        {
        }

        public VisitorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
