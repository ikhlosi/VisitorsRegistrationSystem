using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces
{
    public interface IVisitRepository
    {
        void AddVisit(Visit visit);
        void RemoveVisit(int id);
        void UpdateVisit(Visit visit);
        bool VisitExists(Visit visit);
        bool VisitExists(int id);
        Visit GetVisit(int id);
        IReadOnlyList<Visit> GetVisits();
        void AddVisitor(Visitor visitor);
        void RemoveVisitor(int id);
        void UpdateVisitor(Visitor visitor);
        bool VisitorExists(Visitor visitor);
        bool VisitorExists(int id);
        Visitor GetVisitor(int id);
        List<Visitor> GetAllVisitors();

    }
}
