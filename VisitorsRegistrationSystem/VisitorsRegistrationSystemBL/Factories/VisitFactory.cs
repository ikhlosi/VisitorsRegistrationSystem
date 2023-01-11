using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories {
    /// <summary>
    /// This is a static class that is used for instantiating a new Visit object.
    /// </summary>
    public static class VisitFactory {

        /// <summary>
        /// This methode creates a new Visit object while also defining the required and non required parameters.
        /// </summary>
        /// <returns>A newly created Visit object</returns>
        public static Visit MakeVisit(int? id, Visitor visitor, Company visitedCompany, Employee visitedEmployee) {
			try {
				Visit v = new Visit(visitor, visitedCompany, visitedEmployee, DateTime.Now);
				if (id.HasValue) v.SetId(id.Value);
				return v;
			}
			catch (Exception e){
				VisitException ex = new VisitException("VisitFactory - MakeVisit",e);
				ex.Data.Add("Visit ID", id);
				ex.Data.Add("Visitor", visitor);
				ex.Data.Add("VisitedCompany", visitedCompany);
				ex.Data.Add("VisitedEmployee", visitedEmployee);
				throw ex;
			}
        }
    }
}
