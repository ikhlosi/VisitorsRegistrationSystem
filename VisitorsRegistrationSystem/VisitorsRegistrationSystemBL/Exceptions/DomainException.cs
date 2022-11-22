//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VisitorsRegistrationSystemBL.Exceptions
//{
//    public class DomainException : Exception
//    {
//        private string _exceptionInfo = $"{this.GetType().Name}";
//        public DomainException()
//        {
//        }

//        public DomainException(string? message) : base($"{GenerateExceptionInfo()} {System.Reflection.MethodBase.GetCurrentMethod().Name} - " + message)
//        {
//        }

//        public DomainException(string? message, Exception? innerException) : base(message, innerException)
//        {
//        }

//        private string GenerateExceptionInfo()
//        {
//            return $"{this.GetType()}:";
//        }
//    }
//}
