using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Checkers;
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
        public int CompanyId { get; private set; }

        public void SetId(int id)
        {
            if (id <= 0) throw new EmployeeException("Employee - SetID - invalid ID");
            this.ID = id;
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EmployeeException("Employee - SetName - name is empty");
            this.Name = name;
        }
        public void SetLastName(string lastname)
        {
            if (string.IsNullOrWhiteSpace(lastname)) throw new EmployeeException("Employee - SetLastName - lastname is empty");
            this.LastName = lastname;
        }
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new EmployeeException("Employee - SetEmail - email is empty");
            if (!EmailChecker.IsValid(email)) throw new EmployeeException("Employee - SetEmail - invalid format");
            this.Email = email;
        }
        public void SetFunction(string function)
        {
            if (string.IsNullOrWhiteSpace(function)) throw new EmployeeException("Employee - SetFunction - function is empty");
            this.Function = function;
        }
        public void SetCompanyId(int companyId)
        {
            if (companyId < 1) throw new EmployeeException("Employee - SetCompanyId - invalid ID");
            this.CompanyId = companyId;
        }
        public bool IsSame(Employee otherEmployee) 
        {
            if (otherEmployee == null) throw new EmployeeException("Employee - IsSame - argument is null");
            return (this.ID == otherEmployee.ID) && (this.Name == otherEmployee.Name) && (this.LastName == otherEmployee.LastName) && (this.Email == otherEmployee.Email) && (this.Function == otherEmployee.Function) && (this.CompanyId == otherEmployee.CompanyId);
        }
        
        public override bool Equals(object? obj)
        {
            return obj is Employee employee &&
                   ID == employee.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override string? ToString()
        {
            return this.Name;
        }
    }
}