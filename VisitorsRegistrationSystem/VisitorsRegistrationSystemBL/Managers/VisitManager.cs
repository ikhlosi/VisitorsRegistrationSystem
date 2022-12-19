using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;
using Moq;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class VisitManager
    {
        // todo: consistentie: VisitException gebruiken ipv VisitManagerException
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
            if (visit == null) throw new VisitManagerException("VisitManager(AddVisit) - visit is null");
            if (_repo.VisitExists(visit)) throw new VisitManagerException("VisitManager - AddVisit - Visit does exist");
            _repo.AddVisit(visit);
        }
        public void DeleteVisit(Visit visit)
        {
            if (visit == null) throw new VisitManagerException("VisitManager(Deletevisit) - visit is null");
            if (!_repo.VisitExists(visit)) throw new VisitManagerException("VisitManager - Deletevisit - visit does not exist");
            _repo.RemoveVisit(visit.Id);
        }
        public void UpdateVisit(Visit visit)
        {
            if (visit == null) throw new VisitManagerException("VisitManager(Updatevisit) - visit is null");
            try {
                if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager(Updatevisit) - visit does not exist");
                // wanneer je debugt en in de repo.getvisit probeert te springen springt hij ervoer
                // waardoor je hieronder een nullreference exception krijgt, geen idee waar het fout gaat
                // de methode getvisit werkt nochtans in een gewone consoletest
                Visit visitFromDB = _repo.GetVisit(visit.Id);
                if (visit.IsSame(visitFromDB)) throw new VisitManagerException("VisitManager(Updatevisit) - visit is unchanged");
                _repo.UpdateVisit(visit);
            }
            catch (Exception ex) {
                throw new VisitManagerException("VisitManager - UpdateVisit", ex);
            }
        }
        public Visitor AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitManagerException("VisitManager - Addvisitor - visitor is null");
            try
            {
                if (_repo.VisitorExists(visitor)) return _repo.GetVisitor(visitor.Email);
                return _repo.AddVisitor(visitor);
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
        public Visitor GetVisitor(string email, DateTime endTime)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitManagerException("VisitManager - Getvisitor - email is null");
            try
            {
                return _repo.GetVisitor(email);
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - GetVisitor", ex);
            }
        }
        public void EndVisit(string email, DateTime endTime) {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitException("VisitManager - EndVisit - email is null");
            try {
                if (!_repo.VisitorExists(email)) {
                    throw new VisitManagerException("VisitManager - EndVisit - Visitor is not recognised");
                    // todo: ? ask if necessary: extra connection
                }
                int rowsAffected = _repo.EndVisit(email, endTime);
                if (rowsAffected <= 0) {
                    throw new VisitManagerException("VisitManager - EndVisit - nothing changed");
                    // todo: ? ask if ok: wanneer niets verandert in de DB krijgt de bezoeker een melding; wel pas nadat er connectie gelegd wordt met de DB en de "update" gebeurd is
                }
            }
            catch (Exception ex) {
                throw new VisitException("VisitManager - EndVisit", ex);
            }
        }

        public IReadOnlyList<VisitDTO> GetVisitsByVisitorId(int visitordId)
        {
            if (visitordId <= 0) throw new VisitManagerException("VisitManager - GetVisitsByVisitorId - id is null");
            try
            {
                if (_repo.VisitorExists(visitordId) == false) throw new VisitManagerException("VisitManager - GetVisitsByVisitorId - visitor does not exist");
                return _repo.GetVisitsByVisitorId(visitordId);
            } catch (Exception ex)
            {
                throw new VisitManagerException("VisitManager - GetVisitsByVisitorId",ex);
            }
        }

    }
}
