using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class VisitorManagerException : Exception
    {
        public VisitorManagerException(string? message) : base(message)
        {
        }

        public VisitorManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
