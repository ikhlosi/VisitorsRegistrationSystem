using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain {
    public class Employee 
    {
        public Employee(string name, string lastName, string email, string function)
        {
            SetName(name);
            SetLastName(lastName);
            SetEmail(email);
            Function = function;
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Function { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EmployeeException("SetName - name is empty");
            this.Name = name;
        }
        public void SetLastName(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname)) throw new EmployeeException("SetLastName - lastname is empty");
            this.LastName = lastname;
        }
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new EmployeeException("SetEmail - email is empty");
            // TODO: Email checker 
            this.Email = email;
        }
        public void SetFunction(string function)
        {
            if (string.IsNullOrWhiteSpace(function)) throw new EmployeeException("SetFunction - function is empty");
        }
    }
}