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
        public static IReadOnlyList<CompanyDTO> MapFromCompany (IReadOnlyList<Company> list)
        {
            List<CompanyDTO> map = new List<CompanyDTO> ();
            foreach (Company c in list)
            {
                map.Add(new CompanyDTO(c.ID,c.Name,c.VATNumber,c.Address,c.TelephoneNumber,c.Email));
            }

            return map;
        }

        public static IReadOnlyList<EmployeeDTO> MapFromEmployee (IReadOnlyList<Employee> list)
        {
            List<EmployeeDTO> map = new List<EmployeeDTO>();
            foreach (Employee e in list)
            {
                // Om build errors te vermijden:
                CompanyDTO company = new CompanyDTO(1, "blabla", "abcdefg", new Address("Gent", "kerkstr", "3", "b"), "1234567789", "blabla@abcdefg.com");
                map.Add(new EmployeeDTO(e.ID, e.Name, e.LastName, e.Email, company ,e.Function));
            }

            return map;
        }


    }
}
