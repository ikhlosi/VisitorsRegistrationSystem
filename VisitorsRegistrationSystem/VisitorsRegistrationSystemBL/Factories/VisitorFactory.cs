using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public class VisitorFactory
    {
        public static Visitor MakeVisitor(int? id, string name,  string email, Company visitingCompany)
        {
            try
            {
                Visitor v = new Visitor(name,email);
                if (id.HasValue) v.setId(id.Value);
                if (visitingCompany != null ) v.setVisitorCompany(visitingCompany);
                return v;
            }
            catch
            {
                VisitorException ex = new VisitorException("VisitorFactory - MakeVisitor");
                ex.Data.Add("Visitor id", id);
                ex.Data.Add("Visitor name", name);
                ex.Data.Add("Visitor email", email);
                ex.Data.Add("Visitor visitingCompany", visitingCompany);
                throw ex;
            }
        }
    }
}
