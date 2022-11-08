using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;
namespace VisitorsRegistrationSystemDL.Repositories
{
    public class VisitRepositoryADO : IVisitRepository
    {
        private string connectionString;
        public VisitRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddVisit(Visit visit)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT into Visit(visitorId,startTime,endTime,companyId,employeeId) values (@visitorId,@startTime,@endTime,@companyId,@employeeId)";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@visitorId", visit.Visitor.Id);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", visit.EndTime);
                    cmd.Parameters.AddWithValue("@companyId", visit.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@employeeId", visit.VisitedEmployee.ID);
                    // Query executen
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("Addvisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveVisit(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"DELETE from Visit where visitId = @id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@id", id);
                    // Query executen
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("RemoveVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateVisit(Visit visit)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"UPDATE Visit set visitorId = @visitorId, startTime = @startTime, endTime = @endTime, companyId = @companyId, employeeId = @employeeId where visitId = @visitId";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@visitorId", visit.Visitor.Id);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", visit.EndTime);
                    cmd.Parameters.AddWithValue("@companyId", visit.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@employeeId", visit.VisitedEmployee.ID);
                    cmd.Parameters.AddWithValue("@visitId", visit.Id);
                    // Query executen
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("UpdateVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool VisitExists(Visit visit)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Visit where visitorId= @visitorId AND startTime = @startTime";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@visitorId",visit.Visitor.Id);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    // Query executen
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                    // Data lezen
                    // Value returnen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("VisitExists", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool VisitExists(int iD)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Visit where visitId= @id;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@id", iD);
                    // Query executen
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                    // Data lezen
                    // Value returnen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("VisitExists", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Visit GetVisit(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select v.visitId as vVI, v.startTime as vST, v.endTime as vEN, vi.id as viI, vi.name as viN, vi.email as viE, vi.visitorCompany as viV, e.id as eId, e.firstName as eFN, e.lastName as eLA , e.email as eEM, e.occupation eOC, c.id cId, c.name as cNA, c.VAT as cVA, c.email as cEM, c.telNr AS cTE,a.id as aId, a.street as aST, a.houseNr as aHO, a.bus as aBU, a.city as aCI from Visit v join Visitor vi on v.visitorId = vi.id join Employee e on v.employeeId = e.id join Company c on v.companyId = c.id join Address a on c.addressId = a.id where v.visitId = @visitId";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@visitId", id);
                    // Query executen
                    // Data lezen
                    Visit visit = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int employeeId = (int)reader["eId"];
                        string employeeName = (string)reader["eFN"];
                        string employeeLastName = (string)reader["eLA"];
                        string employeeEmail = (string)reader["eEM"];
                        string employeeFunction = (string)reader["eOC"];
                        int addressId = (int)reader["aID"];
                        string city = (string)reader["aCI"];
                        string street = (string)reader["aST"];
                        string houseNr = (string)reader["aHO"];
                        string busNr = "";
                        if (reader["aBU"] != DBNull.Value)
                        {
                            busNr = (string)reader["aBU"];
                        }
                        int companyId = (int)reader["cId"];
                        string companyName = (string)reader["cNA"];
                        string telNo = (string)reader["cTE"];
                        string companyEmail = (string)reader["cEM"];
                        string vatNo = (string)reader["cVA"];
                        int visitorId = (int)reader["viI"];
                        string visitorName = (string)reader["viN"];
                        string visitorEmail = (string)reader["viE"];
                        string visitorCompany = (string)reader["viV"];
                        int visitId = (int)reader["vVi"];
                        DateTime startTime = (DateTime)reader["vST"];
                        DateTime endTime = (DateTime)reader["vEN"];

                        Employee employee = EmployeeFactory.MakeEmployee(employeeId, employeeName, employeeLastName, employeeEmail, employeeFunction);
                        Address address = new Address(addressId, city, street, houseNr, busNr);
                        Company company = CompanyFactory.MakeCompany(companyId, companyName, vatNo,address,telNo,companyEmail);
                        Visitor visitor = VisitorFactory.MakeVisitor(visitorId, visitorName, visitorEmail, visitorCompany);
                        visit = new Visit(visitId, visitor, company, employee, startTime, endTime);
                    }
                    // Value returnen
                    return visit;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
