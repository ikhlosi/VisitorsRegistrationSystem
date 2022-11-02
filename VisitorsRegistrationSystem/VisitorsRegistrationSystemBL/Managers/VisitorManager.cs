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
        private Dictionary<string, Visitor> _visitors = new Dictionary<string, Visitor>();

        public VisitorManager(IVisitorRepository repo)
        {
            _repo = repo;
        }

        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - Addvisitor - visitor is null");
            if (_repo.VisitorExists(visitor)) throw new VisitorManagerException("VisitorManager - Addvisitor - visitor has already been registered");
            _repo.AddVisitor(visitor);
        }
        public void DeleteVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - DeleteVisitor - visitor is null");
            if (!_repo.VisitorExists(visitor)) throw new VisitorManagerException(" VisitorManager - DeleteVisitor - visitor is not registered");
            _repo.RemoveVisitor(visitor);
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorManagerException("VisitorManager - UpdateVisitor - visitor is null");
            if (!_repo.VisitorExists(visitor)) throw new VisitorManagerException("VisitorManager - UpdateVisitor - visitor is not registered");
            if (_repo.GetVisitor(visitor.Id).Equals(visitor)) throw new VisitorManagerException("VisitorManager - UpdateVisitor - updated visitor is unchanged");
            _repo.UpdateVisitor(visitor);
        }
        public IReadOnlyList<Visitor> GetVisitors()
        {
            return _visitors.Values.ToList().AsReadOnly();
        }

        public Visitor GetVisitor(int id)
        {
            if (id <= 0) throw new VisitorManagerException("VisitorManager - Getvisitor - id is null");
            return _repo.GetVisitor(id);
        }
    }
}
