using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class ParkingManagerException : Exception
    {
        public ParkingManagerException()
        {
        }

        public ParkingManagerException(string? message) : base(message)
        {
        }

        public ParkingManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParkingManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
