using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    public class VisitRepositoryADOException : Exception
    {
        public VisitRepositoryADOException(string? message) : base(message)
        {
        }

        public VisitRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
