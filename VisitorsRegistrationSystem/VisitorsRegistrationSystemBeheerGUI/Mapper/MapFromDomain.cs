using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBeheerGUI.Model;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBeheerGUI.Mapper
{
    public static class MapFromDomain
    {
        public static CompanyDTO MapFromCompany (Company c)
        {
            return new CompanyDTO(c.ID,c.Name,c.VATNumber,c.Address,c.TelephoneNumber,c.Email);
        }

        public static EmployeeDTO MapFromEmployee (Employee e, CompanyDTO c)
        {
            return new EmployeeDTO(e.ID, e.Name, e.LastName, e.Email, c ,e.Function);
        }


    }
}
