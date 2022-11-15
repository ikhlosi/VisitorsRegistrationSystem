using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories {
    public static class VisitFactory {
        public static Visit MakeVisit(int? id, string visitorName, string visitorEmail, string visitorCompany, Company visitedCompany, Employee visitedEmployee, DateTime startTime) {
			try {
				Visit v = new Visit(visitorName, visitorEmail, visitorCompany, visitedCompany, visitedEmployee, startTime);
				if (id.HasValue) v.SetId(id.Value);
				return v;
			}
			catch {
				VisitException ex = new VisitException("VisitFactory - MakeVisit");
				ex.Data.Add("Visit ID", id);
				ex.Data.Add("VisitorName", visitorName);
				ex.Data.Add("VisitorEmail", visitorEmail);
				ex.Data.Add("VisitorCompany", visitedCompany);
				ex.Data.Add("VisitedCompany", visitedCompany);
				ex.Data.Add("VisitedEmployee", visitedEmployee);
				ex.Data.Add("StartTime", startTime);
				throw ex;
			}
        }
    }
}
