using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsRegistrationSystemBL.Exceptions
{
    public class ParkingException : Exception
    {
        public ParkingException(string? message) : base(message)
        {
        }

        public ParkingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
