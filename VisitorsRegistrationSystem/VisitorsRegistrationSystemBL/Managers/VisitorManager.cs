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
    public class VisitorManager
    {
        private IVisitorRepository _repo;

        public VisitorManager(IVisitorRepository repo)
        {
            _repo = repo;
        }

        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - Addvisitor - visitor is null");
            try
            {
                if (_repo.VisitorExists(visitor.Id)) throw new VisitorManagerException("VisitorManager - Addvisitor - visitor has already been registered");
                _repo.AddVisitor(visitor);
            } catch (Exception ex)
            {
                throw new VisitorManagerException("VisitorManager - AddVisitor", ex);
            }
        }
        public void DeleteVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - DeleteVisitor - visitor is null");
            try
            {
                if (!_repo.VisitorExists(visitor.Id)) throw new VisitorManagerException("VisitorManager - DeleteVisitor - visitor is not registered");
                _repo.RemoveVisitor(visitor.Id);
            } catch (Exception ex)
            {
                throw new VisitorManagerException("VisitorManager - DeleteVisitor", ex);
            }
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - UpdateVisitor - visitor is null");
            try
            {
                if (!_repo.VisitorExists(visitor.Id)) throw new VisitorManagerException("VisitorManager - UpdateVisitor - visitor is not registered");
                if (_repo.GetVisitor(visitor.Id).Equals(visitor)) throw new VisitorManagerException("VisitorManager - UpdateVisitor - updated visitor is unchanged");
                _repo.UpdateVisitor(visitor);
            } catch(Exception ex)
            {
                throw new VisitorManagerException("VisitorManager - UpdateVisitor", ex);
            }
        }
        public IReadOnlyList<Visitor> GetVisitors()
        {
            try
            {
                return _repo.GetAllVisitors().AsReadOnly();
            } catch (Exception ex)
            {
                throw new VisitorManagerException("VisitorManager - GetVisitors", ex);
            }
        }

        public Visitor GetVisitor(int id)
        {
            if (id <= 0) throw new VisitorManagerException("VisitorManager - Getvisitor - id is null");
            try
            {
                return _repo.GetVisitor(id);
            } catch (Exception ex)
            {
                throw new VisitorManagerException("VisitorManager - GetVisitor", ex);
            }
        }
    }
}
