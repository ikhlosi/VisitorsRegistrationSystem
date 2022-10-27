using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces
{
    public interface IVisitorRepository
    {
        void AddVisitor(Visitor visitor);
        void RemoveVisitor(Visitor visitor);
        void UpdateVisitor(Visitor visitor);
        bool VisitorExists(Visitor visitor);
        bool VisitorExists(int id);
        Visitor GetVisitor(int id);
        List<Visitor> GetAllVisitors();
    }
}
