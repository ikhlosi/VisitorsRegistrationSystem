using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    public class ParkingRepositoryADOException : Exception
    {
        public ParkingRepositoryADOException()
        {
        }

        public ParkingRepositoryADOException(string? message) : base(message)
        {
        }

        public ParkingRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParkingRepositoryADOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
