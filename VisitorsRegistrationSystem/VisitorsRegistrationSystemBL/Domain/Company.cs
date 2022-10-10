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
        public Company(string name, string vATNumber, Address address, string telephoneNumber, string email) {
            SetName(name);
            SetVATNo(vATNumber);
            SetAddress(address);
            SetTelNo(telephoneNumber);
            SetEmail(email);
        }

        public Company(string name, string vATNumber, string email) {
            SetName(name);
            SetVATNo(vATNumber);
            SetEmail(email);
        }

        public string Name { get; private set; }
        public string VATNumber { get; private set; }
        public Address Address { get; private set; }
        public string TelephoneNumber { get; private set; }
        public string Email { get; private set; }
        // private List<Employee> _employees = new List<Employee>();

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new CompanyException("SetName - name is empty");
            this.Name = name;
        }

        public void SetVATNo(string vatNum) {
            if (string.IsNullOrWhiteSpace(vatNum)) throw new CompanyException("SetVATNo - VAT number is empty");
            // TODO: Checker class  - VAT number check
            this.VATNumber = vatNum;
        }

        public void SetAddress(Address a) {
            if (a == null) throw new CompanyException("SetAddress - Address is null");
            this.Address = a;
        }

        public void SetTelNo(string telNo) { 
            if (string.IsNullOrWhiteSpace(telNo)) throw new CompanyException("SetTelNo - telephone number is empty");
            this.TelephoneNumber = telNo;
        }

        public void SetEmail (string email) {
            if (string.IsNullOrWhiteSpace(email)) throw new CompanyException("SetEmail - email is empty");
            // TODO: Checker class - Email check
            this.Email = email;
        }
    }
}
