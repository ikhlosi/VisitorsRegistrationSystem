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
    /// This is a static class that is used for instantiating a new Visitor object.
    /// </summary>
    public static class VisitorFactory
    {

        /// <summary>
        /// This methode creates a new Visitor object while also defining the required and non required parameters.
        /// </summary>
        /// <returns>A newly created Visitor object</returns>
        public static Visitor MakeVisitor(int? id, string name,  string email, string visitorCompany)
        {
            try
            {
                Visitor v = new Visitor(name, email, visitorCompany);
                if (id.HasValue) v.SetId(id.Value);
                return v;
            }
            catch
            {
                VisitorException ex = new VisitorException("VisitorFactory - MakeVisitor");
                ex.Data.Add("Visitor id", id);
                ex.Data.Add("Visitor name", name);
                ex.Data.Add("Visitor email", email);
                ex.Data.Add("Visitor visitorCompany", visitorCompany);
                throw ex;
            }
        }
    }
}