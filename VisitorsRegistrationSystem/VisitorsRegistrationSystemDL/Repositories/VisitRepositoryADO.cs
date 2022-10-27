using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemDL.Repositories
{
    internal class VisitRepositoryADO : IVisitRepository
    {
        private string _connectionString;
     public VisitRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddVisit(Visit visit)
        {
            throw new NotImplementedException();
        }

        public Visitor GetVisit(int iD)
        {
            throw new NotImplementedException();
        }

        public void RemoveVisit(Visit visit)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisit(Visit visit)
        {
            throw new NotImplementedException();
        }

        public bool VisitExists(Visit visit)
        {
            throw new NotImplementedException();
        }

        public bool VisitExists(int iD)
        {
            throw new NotImplementedException();
        }
    }
}
