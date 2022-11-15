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
        //private Dictionary<int, Visit> _visits = new Dictionary<int, Visit>();

        public IReadOnlyList<Visit> GetVisits()
        {
            return _repo.GetVisits();
        }

        public void AddVisit(Visit visit)
        {
            //TODO: Check if employee is part of company, when repositories classes are made
            if (visit == null) throw new VisitException("VisitManager(AddVisit) - visit is null");
            if (_repo.VisitExists(visit)) throw new VisitException("VisitManager - AddVisit - Visit does exist");
            _repo.AddVisit(visit);
        }

        public void DeleteVisit(Visit visit)
        {
            if (visit == null) throw new VisitException("VisitManager(Deletevisit) - visit is null");
            if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager - Deletevisit - visit does not exist");
            _repo.RemoveVisit(visit.Id);
        }

        public void UpdateVisit(Visit visit)
        {
            if (visit == null) throw new VisitException("VisitManager(Updatevisit) - visit is null");
            try {
                if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager(Updatevisit) - visit does not exist");
                Visit visitFromDB = _repo.GetVisit(visit.Id);
                if (visit.IsSame(visitFromDB)) throw new VisitException("VisitManager(Updatevisit) - visit is unchanged");
                _repo.UpdateVisit(visit);
            }
            catch (Exception ex) {
                throw new VisitException("VisitManager - UpdateVisit", ex);
            }
        }

    }
}
