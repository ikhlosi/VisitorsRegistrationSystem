using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class VisitRepositoryADO : IVisitRepository
    {
        private string connectionString;
        private VisitorRepositoryADO _visitorRepo;
        private CompanyRepositoryADO _companyRepo;

        public VisitRepositoryADO(string connectionString, VisitorRepositoryADO visitorRepo)
        {
            this.connectionString = connectionString;
            _visitorRepo = visitorRepo;
        }

        public void AddVisit(Visit visit)
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

        public Visit GetVisit(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM visit WHERE id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int visitorId = (int)reader["visitorId"];
                    Visitor v = _visitorRepo.GetVisitor(visitorId);
                    Company c = _companyRepo.GetCompany(id);
                    //TODO
                   // Visit visit = new Visit(v, c,/,/,/,/);
                    reader.Close();
                    return visit;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOExceptions("GetVisit");
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
