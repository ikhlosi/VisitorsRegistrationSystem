using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBeheerGUI.Model
{
    public class CompanyDTO
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string VATNumber { get; private set; }
        public Address Address { get; private set; }
        public string TelephoneNumber { get; private set; }
        public string Email { get; private set; }

        public CompanyDTO(int iD, string name, string vATNumber, Address address, string telephoneNumber, string email)
        {
            ID = iD;
            Name = name;
            VATNumber = vATNumber;
            Address = address;
            TelephoneNumber = telephoneNumber;
            Email = email;
        }

        public override string? ToString()
        {
            return this.Name;
        }
    }
}
