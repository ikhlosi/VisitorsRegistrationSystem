using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Managers
{
    public class VisitorManager
    {
        private Dictionary<string, Visitor> _visitors = new Dictionary<string, Visitor>();
        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorException("VisitorManager - Addvisitor - visitor is null");
            if (_visitors.ContainsKey(visitor.Name)) throw new VisitorException("VisitorManager - Addvisitor - visitor has already been registered");
            _visitors.Add(visitor.Name, visitor);
        }
        public void DeleteVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorException("VisitorManager - DeleteVisitor - visitor is null");
            if (!_visitors.ContainsKey(visitor.Name)) throw new VisitorException(" VisitorManager - DeleteVisitor - visitor is not registered");
            _visitors.Remove(visitor.Name);
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new VisitorException("VisitorManager - UpdateVisitor - visitor is null");
            if (!_visitors.ContainsKey(visitor.Name)) throw new VisitorException("VisitorManager - UpdateVisitor - visitor is not registered");
            if (_visitors[visitor.Name].Equals(visitor)) throw new VisitorException("VisitorManager - UpdateVisitor - updated visitor is unchanged");
            _visitors[visitor.Name] = visitor;
        }
        public IReadOnlyList<Visitor> GetVisitors()
        {
            return _visitors.Values.ToList().AsReadOnly();
        }
    }
}
