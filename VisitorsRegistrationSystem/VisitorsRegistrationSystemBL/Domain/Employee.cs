using System.Xml.Linq;
using VisitorsRegistrationSystemBL.Checkers;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain {
    /// <summary>
    /// This class represents an employee.
    /// </summary>
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
        /// <summary>
        /// This method checks whether the properties of this employee object
        /// are the same as the properties of another employee object.
        /// </summary>
        /// <param name="otherEmployee">The other employee to compare with.</param>
        /// <returns>A bool indicating whether the properties of both objects are equal.</returns>
        /// <exception cref="EmployeeException">
        /// Thrown when the argument is null.
        /// </exception>
        public bool IsSame(Employee otherEmployee) 
        {
            if (otherEmployee == null) throw new EmployeeException("Employee - IsSame - argument is null");
            return (this.ID == otherEmployee.ID) && (this.Name == otherEmployee.Name) && (this.LastName == otherEmployee.LastName) && (this.Email == otherEmployee.Email) && (this.Function == otherEmployee.Function) && (this.CompanyId == otherEmployee.CompanyId);
        }
        
        /// <summary>
        /// This method compares 2 employee objects to indicate equality.
        /// The objects are considered equal if their ID property is the same.
        /// </summary>
        /// <param name="obj">The employee object to compare with.</param>
        /// <returns>A bool indicating whether the objects are equal.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Employee employee &&
                   ID == employee.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        /// <summary>
        /// This method gives the string representation of an employee object.
        /// </summary>
        /// <returns>A string containing the name of the employee.</returns>
        public override string? ToString()
        {
            return this.Name;
        }
    }
}