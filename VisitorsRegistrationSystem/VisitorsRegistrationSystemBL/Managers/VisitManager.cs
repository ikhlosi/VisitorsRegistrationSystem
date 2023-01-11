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
        private IVisitRepository _repo;

        public VisitManager(IVisitRepository repo)
        {
            _repo = repo;
        }


        /// <summary>
        /// This method return all visits from the repository.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<VisitDTO> GetVisits()
        {
             return _repo.GetVisits();
        }
        /// <summary>
        /// This method throws an exception if visit is null or if the visit does not exist.
        /// If no exception is thrown the visit is added.
        /// </summary>
        /// <param name="visit"></param>
        /// <exception cref="VisitManagerException"></exception>
        public void AddVisit(Visit visit)
        {
            if (visit == null) throw new VisitManagerException("VisitManager(AddVisit) - visit is null");
            if (_repo.VisitExists(visit)) throw new VisitManagerException("VisitManager - AddVisit - Visit does exist");
            _repo.AddVisit(visit);
        }
        /// <summary>
        /// This method throws an exception if visitDTO is null or if the visit does not exist.
        /// If no exception is thrown this method remove the visitby visitId.
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <exception cref="VisitManagerException"></exception>
        public void DeleteVisit(VisitDTO visitDTO)
        {
            if (visitDTO == null) throw new VisitManagerException("VisitManager(Deletevisit) - visit is null");
            if (!_repo.VisitExists(visitDTO.visitor.Id, visitDTO.startTime)) throw new VisitManagerException("VisitManager - Deletevisit - visit does not exist");
            _repo.RemoveVisit(visitDTO.visitId);
        }
        /// <summary>
        /// This method throws an exception when visit is null.
        /// This method also throws an exception when visit is not found or when visit is not changed.
        /// If no exception is thrown this method updates the visit.
        /// </summary>
        /// <param name="visit"></param>
        /// <exception cref="VisitManagerException"></exception>
        public void UpdateVisit(Visit visit)
        {
            if (visit == null) throw new VisitManagerException("VisitManager(Updatevisit) - visit is null");
            try {
                if (!_repo.VisitExists(visit)) throw new VisitException("VisitManager(Updatevisit) - visit does not exist");
                Visit visitFromDB = _repo.GetVisit(visit.Id);
                if (visit.IsSame(visitFromDB)) throw new VisitManagerException("VisitManager(Updatevisit) - visit is unchanged");
                _repo.UpdateVisit(visit);
            }
            catch (Exception ex) {
                throw new VisitManagerException("VisitManager - UpdateVisit", ex);
            }
        }
        /// <summary>
        /// This method throws an exception if visitor is null.
        /// If visitor is not null it will add the visitor.
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns>The added visitor or visitor that is already in the database </returns>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if visitor is null or when the visitor id is not found.
        /// If no exception is thrown this will remove the visitor by id.
        /// </summary>
        /// <param name="visitor"></param>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if visitor is null.
        /// This method also throws an exception if visitor is not found by id or if it has not been changed.
        /// If no exception is thrown this method will update the visitor.
        /// </summary>
        /// <param name="visitor"></param>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This method will return all visitors that are registered in the database.
        /// </summary>
        /// <returns>all visitors in the database</returns>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This throws an exception when id is 0 or lower.
        /// If no exception is thrown this will return a visitor with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>visitor id</returns>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the email of a visitor is null or empty.
        /// If no exception is thrown it will return the visitor with the given email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="endTime"></param>
        /// <returns>visitor email</returns>
        /// <exception cref="VisitManagerException"></exception>
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
        /// <summary>
        /// This method throws an exception if the email is null or empty.
        /// This method will look for a visitor by email if this is not found it will throw an exception.
        /// If no method is thrown this method will register the email and endtime of a visit.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="endTime"></param>
        /// <exception cref="VisitException"></exception>
        public void EndVisit(string email, DateTime endTime) {
            if (string.IsNullOrWhiteSpace(email)) throw new VisitException("VisitManager - EndVisit - email is null");
            try {
                if (!_repo.VisitorExists(email)) {
                    throw new VisitManagerException("VisitManager - EndVisit - Visitor is not recognised");
                }
                int rowsAffected = _repo.EndVisit(email, endTime);
                if (rowsAffected <= 0) {
                    throw new VisitManagerException("VisitManager - EndVisit - nothing changed");
                }
            }
            catch (Exception ex) {
                throw new VisitException("VisitManager - EndVisit", ex);
            }
        }
        /// <summary>
        /// This method throws an exception if visitorId is 0 or lower.
        /// This method also throws an exception if visitor is not found on visitorId.
        /// If no exception is thrown tis method will return a visitor by visitorId.
        /// </summary>
        /// <param name="visitordId"></param>
        /// <returns>visitorId</returns>
        /// <exception cref="VisitManagerException"></exception>

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
