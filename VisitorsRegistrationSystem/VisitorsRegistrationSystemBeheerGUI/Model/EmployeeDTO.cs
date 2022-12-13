using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBeheerGUI.Model
{
    public class EmployeeDTO
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public CompanyDTO Company { get; private set; }
        public string Function { get; private set; }


        public EmployeeDTO(int iD, string name, string lastName, string email, CompanyDTO company, string function)
        {
            ID = iD;
            Name = name;
            LastName = lastName;
            Email = email;
            Company = company;
            Function = function;
        }
    }
}
