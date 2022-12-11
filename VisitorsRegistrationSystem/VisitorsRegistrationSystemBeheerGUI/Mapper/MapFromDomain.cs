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
                map.Add(new EmployeeDTO(e.ID, e.Name, e.LastName, e.Email, ,e.Function));
            }

            return map;
        }


    }
}
