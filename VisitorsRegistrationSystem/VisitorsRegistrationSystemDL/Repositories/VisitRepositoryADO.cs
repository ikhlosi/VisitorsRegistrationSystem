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
    //public class VisitRepositoryADO : IVisitRepository
    //{
    //    private string connectionString;
    //    private VisitorRepositoryADO _visitorRepo;
    //    private CompanyRepositoryADO _companyRepo;

    //    public VisitRepositoryADO(string connectionString, VisitorRepositoryADO visitorRepo)
    //    {
    //        this.connectionString = connectionString;
    //        _visitorRepo = visitorRepo;
    //    }

    //    public void AddVisit(Visit visit)
    //    {
    //        throw new NotImplementedException();
    //    }



    //    public void RemoveVisit(Visit visit)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateVisit(Visit visit)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool VisitExists(Visit visit)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool VisitExists(int iD)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Visit GetVisit(int id, DateTime startTime)
    //    {
    //        SqlConnection connection = new SqlConnection(connectionString);
    //        string query = "SELECT * FROM visit WHERE id=@id";
    //        using (SqlCommand cmd = connection.CreateCommand())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                cmd.CommandText = query;
    //                cmd.Parameters.AddWithValue("@id", id);
    //                IDataReader reader = cmd.ExecuteReader();
    //                reader.Read();
    //                int visitorId = (int)reader["visitorId"];
    //                DateTime visitorStartTime = (DateTime)reader["startTime"];
    //                DateTime visitorEndtTime = (DateTime)reader["endTime"];
    //                int companyId = (int)reader["companyId"];
    //                int employeeId = (int)reader["employeeId"];
    //                Company company = GetCompany(companyId);
    //                Visit visit = new Visit();
    //                reader.Close();
    //                return visit;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new VisitRepositoryADOExceptions("GetVisit");
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //    }
    //}
}
