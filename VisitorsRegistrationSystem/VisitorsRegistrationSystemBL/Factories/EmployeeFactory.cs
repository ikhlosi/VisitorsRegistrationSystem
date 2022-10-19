using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public static class EmployeeFactory
    {
        public static Employee MakeEmployee(int? id, string name, string lastName, string email, string function)
        {
            try {
                Employee e = new Employee(name, lastName, function);
                if (id.HasValue) e.SetId(id.Value);
                if (!string.IsNullOrWhiteSpace(email)) e.SetEmail(email);
                return e;
            }
            catch
            {
                EmployeeException ex = new EmployeeException("EmployeeFactory - MakeEmployee");
                ex.Data.Add("Employee id", id);
                ex.Data.Add("Employee name", name);
                ex.Data.Add("Employee lastName", lastName);
                ex.Data.Add("Employee email", email);
                ex.Data.Add("Employee function", function);
                throw ex;
            }
        }
    }
}
