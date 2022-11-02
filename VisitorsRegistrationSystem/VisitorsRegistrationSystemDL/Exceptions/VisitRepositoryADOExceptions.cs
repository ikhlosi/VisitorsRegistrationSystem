using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    internal class VisitRepositoryADOExceptions : Exception
    {
        public VisitRepositoryADOExceptions(string? message) : base(message)
        {
        }

        public VisitRepositoryADOExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
