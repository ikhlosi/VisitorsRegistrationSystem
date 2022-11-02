using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Factories
{
    public static class VisitorFactory
    {
        public static Visitor MakeVisitor(int? id, string name,  string email, string visitorCompany)
        {
            try
            {
                Visitor v = new Visitor(name,email);
                if (id.HasValue) v.setId(id.Value);
                if (visitorCompany != null ) v.setVisitorCompany(visitorCompany);
                return v;
            }
            catch
            {
                VisitorException ex = new VisitorException("VisitorFactory - MakeVisitor");
                ex.Data.Add("Visitor id", id);
                ex.Data.Add("Visitor name", name);
                ex.Data.Add("Visitor email", email);
                ex.Data.Add("Visitor visitingCompany", visitorCompany);
                throw ex;
            }
        }
    }
}
