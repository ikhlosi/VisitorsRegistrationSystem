using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Domain
{
    public class Company
    {
        // Overload - to instantiate class with only the core attributes
        // ? maybe better to add null checks in above constructor instead, for scalability
        // -> solved with CompanyFactory class
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

        internal void SetID(int id) {
            if (id <= 0) throw new CompanyException("Company - SetID - invalid ID");
            this.ID = id;
        }

        internal void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new CompanyException("SetName - name is empty");
            this.Name = name;
        }

        internal void SetVATNo(string vatNum) {
            if (string.IsNullOrWhiteSpace(vatNum)) throw new CompanyException("SetVATNo - VAT number is empty");
            // TODO: Checker class  - VAT number check
            this.VATNumber = vatNum;
        }

        internal void SetAddress(Address a) {
            if (a == null) throw new CompanyException("SetAddress - Address is null");
            this.Address = a;
        }

        internal void SetTelNo(string telNo) { 
            if (string.IsNullOrWhiteSpace(telNo)) throw new CompanyException("SetTelNo - telephone number is empty");
            this.TelephoneNumber = telNo;
        }

        internal void SetEmail (string email) {
            if (string.IsNullOrWhiteSpace(email)) throw new CompanyException("SetEmail - email is empty");
            // TODO: Checker class - Email check
            this.Email = email;
        }

        public void AddEmployee(Employee employee) {
            if (employee == null) throw new CompanyException("Company - AddEmployee - employee is null");
            if (_employees.Contains(employee)) throw new CompanyException("Company - AddEmployee - employee already exists"); // TODO: Equals & GetHashCode Employee
            this._employees.Add(employee);
        }

        // public void RemoveEmployee(Employee employee) { }

        // public void UpdateEmployee(Employee employee) { }

        // todo: summary
        public bool IsSame(Company otherCompany) {
            // if (otherCompany == null) throw new CompanyException("Company - IsSame - argument is null", new ArgumentNullException());
            if (otherCompany == null) throw new CompanyException("Company - IsSame - argument is null"); // todo: ask if this is necessary. If argument is null, then won't it return false anyway?
            return (this.ID == otherCompany.ID) && (this.Name == otherCompany.Name) && (this.VATNumber == otherCompany.VATNumber) && (this.Address == otherCompany.Address) && (this.TelephoneNumber == otherCompany.TelephoneNumber) && (this.Email == otherCompany.Email);
        }

        public override bool Equals(object? obj) {
            return obj is Company company &&
                   ID == company.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }
    }
}
