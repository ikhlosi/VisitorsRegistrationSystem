using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class VisitManager
    {
        private Dictionary<string, Visit> _visits = new Dictionary<string, Visit>();

        public IReadOnlyList<Visit> GetVisits()
        {
            return _visits.Values.ToList().AsReadOnly();
        }

        public void AddVisit(Visit visit)
        {
            //TODO: Check if employee is part of company, when repositories classes are made
            if (visit == null) throw new VisitorException("VisitorManager - Addvisitor - visitor is null");
            _visits.Add(visit.GetHashCode().ToString(), visit);
        }

        public void DeleteVisit(Visit visit)
        {
            if (visit == null) throw new VisitorException("VisitorManager - Addvisitor - visitor is null");
            _visits.Remove(visit.GetHashCode().ToString());
        }

        public void UpdateVisit(Visit visit)
        {
            if (visit == null) throw new VisitorException("VisitorManager - Addvisitor - visitor is null");
            _visits[visit.GetHashCode().ToString()] = visit;
        }

    }
}
