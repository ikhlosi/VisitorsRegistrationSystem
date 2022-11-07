using System.Runtime.Serialization;

namespace VisitorsRegistrationSystemDL.Exceptions
{
    public class EmployeeRepositoryADOException : Exception
    {
        public EmployeeRepositoryADOException()
        {
        }

        public EmployeeRepositoryADOException(string? message) : base(message)
        {
        }

        public EmployeeRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}