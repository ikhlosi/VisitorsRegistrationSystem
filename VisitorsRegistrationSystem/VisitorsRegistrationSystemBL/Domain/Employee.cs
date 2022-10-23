using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain {
    public class Employee 
    {
        internal Employee(string name, string lastName, string function)
        {
            SetName(name);
            SetLastName(lastName);
            SetFunction(function);
        }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Function { get; private set; }

        public void SetId(int id)
        {
            if (id <= 0) throw new EmployeeException("Employee - SetID - invalid ID");
            this.ID = id;
        }
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
            this.Function = function;
        }

        internal bool IsSame(Employee employee) {
            throw new NotImplementedException();
        }
    }
}