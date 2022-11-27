using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories {
    public static class VisitFactory {
        public static Visit MakeVisit(int? id, Visitor visitor, Company visitedCompany, Employee visitedEmployee, DateTime startTime) {
			try {
				Visit v = new Visit(visitor, visitedCompany, visitedEmployee, startTime);
				if (id.HasValue) v.SetId(id.Value);
				return v;
			}
			catch (Exception e){
				VisitException ex = new VisitException("VisitFactory - MakeVisit",e);
				ex.Data.Add("Visit ID", id);
				ex.Data.Add("Visitor", visitor);
				ex.Data.Add("VisitedCompany", visitedCompany);
				ex.Data.Add("VisitedEmployee", visitedEmployee);
				ex.Data.Add("StartTime", startTime);
				throw ex;
			}
        }
    }
}
