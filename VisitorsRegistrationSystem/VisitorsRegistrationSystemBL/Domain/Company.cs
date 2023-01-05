using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Checkers;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Company
    {
        internal Company(string name, string vATNumber, string email) {
            SetName(name);
            SetVATNo(vATNumber);
            SetEmail(email);
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public string VATNumber { get; private set; }
        public Address Address { get; private set; }
        public string TelephoneNumber { get; private set; }
        public string Email { get; private set; }
        private List<Employee> _employees = new List<Employee>();
        //private Dictionary<int, Employee> _employees = new Dictionary<int, Employee>(); // key is Employee ID

        internal void SetID(int id) {
            if (id <= 0) throw new CompanyException("Company - SetID - invalid ID");
            this.ID = id;
        }

        internal void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new CompanyException("Company - SetName - name is empty");
            this.Name = name;
        }

        internal void SetVATNo(string vatNum) {
            if (string.IsNullOrWhiteSpace(vatNum)) throw new CompanyException("Company - SetVATNo - VAT number is empty");
            // if (!VATChecker.IsValid(vatNum)) throw new CompanyException("Company - SetVATNo - invalid VAT number");
            this.VATNumber = vatNum;
        }

        internal void SetAddress(Address a) {
            if (a == null) throw new CompanyException("Company - SetAddress - Address is null");
            this.Address = a;
        }

        internal void SetTelNo(string telNo) { 
            if (string.IsNullOrWhiteSpace(telNo)) throw new CompanyException("Company - SetTelNo - telephone number is empty");
            this.TelephoneNumber = telNo;
        }

        internal void SetEmail (string email) {
            if (string.IsNullOrWhiteSpace(email)) throw new CompanyException("Company - SetEmail - email is empty");
            // if (!EmailChecker.IsValid(email)) throw new CompanyException("Company - SetEmail - invalid format");
            this.Email = email;
        }

        public void AddEmployee(Employee employee) {
            if (employee == null) throw new CompanyException("Company - AddEmployee - employee is null");
            if (_employees.Contains(employee)) throw new CompanyException("Company - AddEmployee - employee already exists");
            this._employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee) {
            if (employee == null) throw new CompanyException("Company - RemoveEmployee - employee is null");
            if (!_employees.Contains(employee)) throw new CompanyException("Company - RemoveEmployee - employee doesn't exists");
            this._employees.Remove(employee);
        }

        public void UpdateEmployee(Employee employee) {
            if (employee == null) throw new CompanyException("Company - UpdateEmployee - employee is null");
            if (!_employees.Contains(employee)) throw new CompanyException("Company - UpdateEmployee - employee doesn't exists");
            int indexOfEmployeeToUpdate = this._employees.IndexOf(employee);
            Employee employeeToUpdate = this._employees[indexOfEmployeeToUpdate];
            if (employeeToUpdate.IsSame(employee)) throw new CompanyException("Company - UpdateEmployee - nothing to update");
            this._employees[indexOfEmployeeToUpdate] = employee;
        }

        // todo: summary
        public bool IsSame(Company otherCompany) {
            // if (otherCompany == null) throw new CompanyException("Company - IsSame - argument is null", new ArgumentNullException());
            if (otherCompany == null) throw new CompanyException("Company - IsSame - argument is null");
            return (this.ID == otherCompany.ID) && (this.Name == otherCompany.Name) && (this.VATNumber == otherCompany.VATNumber) && (this.TelephoneNumber == otherCompany.TelephoneNumber) && (this.Email == otherCompany.Email);
        }

        public override bool Equals(object? obj) {
            return obj is Company company &&
                   ID == company.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        public IReadOnlyList<Employee> GetEmployees()
        {
            return _employees.AsReadOnly();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
