using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class VisitManager
    {
        private IVisitRepository _repo;
        private Dictionary<int, Visit> _visits = new Dictionary<int, Visit>();

        public IReadOnlyList<Visit> GetVisits()
        {
            return _visits.Values.ToList().AsReadOnly();
        }

        public void AddVisit(Visit visit)
        {
            //TODO: Check if employee is part of company, when repositories classes are made
            if (visit == null) throw new VisitException("VisitManager(AddVisit) - visit is null");
            if (_repo.VisitExists(visit)) throw new VisitException("VisitManager - AddVisit - visitor does exist");
            _repo.AddVisit(visit);
        }

        public void DeleteVisit(Visit visit)
        {
            if (visit == null) throw new VisitException("VisitManager(Deletevisit) - visit is null");
            if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager - Deletevisit - visit does not exist");
            _repo.RemoveVisit(visit);
        }

        public void UpdateVisit(Visit visit)
        {
            if (visit == null) throw new VisitException("VisitManager(Updatevisit) - visit is null");
            if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager(Updatevisit) - visit does exist");
            if (_repo.GetVisit(visit.Id).Equals(visit)) throw new VisitException("VisitManager(Updatevisit) - visit is unchanged");
            _repo.UpdateVisit(visit);
        }

    }
}
