using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    internal class EmployeeException : Exception
    {
        public EmployeeException()
        {
        }

        public EmployeeException(string? message) : base(message)
        {
        }

        public EmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
