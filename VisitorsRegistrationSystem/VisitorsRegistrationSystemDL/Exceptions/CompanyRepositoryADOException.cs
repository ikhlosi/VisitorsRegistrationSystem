using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    public class CompanyRepositoryADOException : Exception
    {
        public CompanyRepositoryADOException()
        {
        }

        public CompanyRepositoryADOException(string? message) : base(message)
        {
        }

        public CompanyRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
