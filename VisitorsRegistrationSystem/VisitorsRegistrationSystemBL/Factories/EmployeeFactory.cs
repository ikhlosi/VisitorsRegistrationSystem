using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    /// <summary>
    /// This is a static class that is used for generating a new Employee object.
    /// </summary>
    public static class EmployeeFactory
    {
        /// <summary>
        /// This methode creates a new Employee object while also defining the required and non required parameters.
        /// </summary>
        /// <returns>A newly created Employee object</returns>
        public static Employee MakeEmployee(int? id, string name, string lastName, string email, string function,int? companyId)
        {
            try {
                Employee e = new Employee(name, lastName, function);
                if (id.HasValue) e.SetId(id.Value);
                if (!string.IsNullOrWhiteSpace(email)) e.SetEmail(email);
                if (companyId.HasValue) e.SetCompanyId(companyId.Value);
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
                ex.Data.Add("Employee companyId", companyId);
                throw ex;
            }
        }
    }
}
