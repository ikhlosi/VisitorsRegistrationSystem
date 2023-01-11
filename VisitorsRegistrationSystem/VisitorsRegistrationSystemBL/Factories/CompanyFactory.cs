using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories {

    /// <summary>
	/// This is a static class that is used for generating a new Company object.
	/// </summary>
	public static class CompanyFactory {

		/// <summary>
		/// This methode creates a new Company object while also defining the required and non required parameters.
		/// </summary>
		/// <returns>A newly created Company object</returns>
        public static Company MakeCompany(int? id, string name, string vatNo, Address address, string telNo, string email) {
			try {
				Company c = new Company(name, vatNo, email);
				if (id.HasValue) c.SetID(id.Value);
				if (address != null) c.SetAddress(address);
				if (!string.IsNullOrEmpty(telNo)) c.SetTelNo(telNo);
				return c;
			}
			catch {
				CompanyException ex = new CompanyException("CompanyFactory - MakeCompany");
				ex.Data.Add("Company name", name);
				ex.Data.Add("Company VAT number", vatNo);
				ex.Data.Add("Company e-mail", email);
				ex.Data.Add("Company address", address);
				ex.Data.Add("Company telephone number", telNo);
				throw ex;
			}
        }
    }
}