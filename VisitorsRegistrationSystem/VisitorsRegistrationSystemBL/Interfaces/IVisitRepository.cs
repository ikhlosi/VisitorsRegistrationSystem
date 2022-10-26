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
        void RemoveVisit(Visit visit);
        void UpdateVisit(Visit visit);
        bool VisitExists(Visit visit);
        bool VisitExists(int id);
        Visit GetVisit(int id);

    }
}
