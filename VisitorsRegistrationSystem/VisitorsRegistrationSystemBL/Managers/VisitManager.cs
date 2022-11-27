using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class VisitManager
    {
        private IVisitRepository _repo;

        public VisitManager(IVisitRepository repo)
        {
            _repo = repo;
        }

        //private Dictionary<int, Visit> _visits = new Dictionary<int, Visit>();

        public IReadOnlyList<VisitDTO> GetVisits()
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

        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitManagerException("VisitManager - Addvisitor - visitor is null");
            try
            {
                if (_repo.VisitorExists(visitor.Id)) throw new VisitManagerException("VisitManager - Addvisitor - visitor has already been registered");
                _repo.AddVisitor(visitor);
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - AddVisitor", ex);
            }
        }
        public void DeleteVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitManagerException("VisitManager - DeleteVisitor - visitor is null");
            try
            {
                if (!_repo.VisitorExists(visitor.Id)) throw new VisitManagerException("VisitManager - DeleteVisitor - visitor is not registered");
                _repo.RemoveVisitor(visitor.Id);
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - DeleteVisitor", ex);
            }
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitManagerException("VisitManager - UpdateVisitor - visitor is null");
            try
            {
                if (!_repo.VisitorExists(visitor.Id)) throw new VisitManagerException("VisitManager - UpdateVisitor - visitor is not registered");
                if (_repo.GetVisitor(visitor.Id).Equals(visitor)) throw new VisitManagerException("VisitManager - UpdateVisitor - updated visitor is unchanged");
                _repo.UpdateVisitor(visitor);
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - UpdateVisitor", ex);
            }
        }
        public IReadOnlyList<Visitor> GetVisitors()
        {
            try
            {
                return _repo.GetAllVisitors().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - GetVisitors", ex);
            }
        }

        public Visitor GetVisitor(int id)
        {
            if (id <= 0) throw new VisitManagerException("VisitManager - Getvisitor - id is null");
            try
            {
                return _repo.GetVisitor(id);
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - GetVisitor", ex);
            }
        }

        }
}
