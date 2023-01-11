using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.DTO;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;
namespace VisitorsRegistrationSystemDL.Repositories
{
    /// <summary>
    /// This is the class responsible for sending queries pertaining
    /// the management of visits in the database.
    /// It implements the IVisitRepository interface.
    /// </summary>
    public class VisitRepositoryADO : IVisitRepository
    {
        private string connectionString;
        public VisitRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// This method inserts a visit into the MySQL database.
        /// </summary>
        /// <param name="visit">The visit object to insert.</param>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void AddVisit(Visit visit)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Connection = connection; cmd.Transaction = transaction;

                    cmd.CommandText = "INSERT INTO Visitor(name,email,visitorCompany) VALUES (@visitorName,@visitorEmail,@visitorCompany);";
                    cmd.Parameters.AddWithValue("@visitorName", visit.Visitor.Name);
                    cmd.Parameters.AddWithValue("@visitorEmail", visit.Visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visit.Visitor.VisitorCompany);
                    cmd.ExecuteNonQuery();
                    long visitorId = cmd.LastInsertedId;

                    cmd.CommandText = "INSERT INTO Visit(visitorId,startTime,companyId,employeeId) VALUES (@visitorId,@startTime,@companyId,@employeeId);";
                    cmd.Parameters.AddWithValue("@visitorId", visitorId);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    cmd.Parameters.AddWithValue("@companyId", visit.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@employeeId", visit.VisitedEmployee.ID);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("Addvisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method removes a visit from the database.
        /// </summary>
        /// <param name="id">The ID of the visit to remove.</param>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void RemoveVisit(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Visit set visible=0 where visitId = @id and visible=1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("RemoveVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method updates a visit in the database.
        /// </summary>
        /// <param name="visit">The updated visit object.</param>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void UpdateVisit(Visit visit)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"UPDATE Visit set visitorId = @visitorId, startTime = @startTime, endTime = @endTime, companyId = @companyId, employeeId = @employeeId where visitId = @visitId";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", visit.Visitor.Id);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    cmd.Parameters.AddWithValue("@companyId", visit.VisitedCompany.ID);
                    cmd.Parameters.AddWithValue("@employeeId", visit.VisitedEmployee.ID);
                    cmd.Parameters.AddWithValue("@visitId", visit.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("UpdateVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain visit exists in the database.
        /// </summary>
        /// <param name="visit">The visit object to check.</param>
        /// <returns>A bool indicating whether the visit exists or not.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool VisitExists(Visit visit)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Visit where visitorId= @visitorId AND startTime = @startTime AND visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", visit.Visitor.Id);
                    cmd.Parameters.AddWithValue("@startTime", visit.StartTime);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("VisitExists", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain visit exists in the database.
        /// </summary>
        /// <param name="visitorId">The ID of the visit to check.</param>
        /// <param name="startTime">The start time of the visit.</param>
        /// <returns>A bool indicating whether the visit exists in the database.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool VisitExists(int visitorId, DateTime startTime)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Visit where visitorId= @visitorId AND startTime = @startTime AND visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", visitorId);
                    cmd.Parameters.AddWithValue("@startTime", startTime);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("VisitExists", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves a visit from the database.
        /// </summary>
        /// <param name="id">The ID of the visit to be retrieved.</param>
        /// <returns>The visit object matching the given ID.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Visit GetVisit(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select v.visitId as vVI, v.startTime as vST, v.endTime as vEN, vi.id as viI, vi.name as viN, vi.email as viE, vi.visitorCompany as viV, e.id as eId, e.firstName as eFN, e.lastName as eLA , e.email as eEM, e.occupation eOC, c.id cId, c.name as cNA, c.VAT as cVA, c.email as cEM, c.telNr AS cTE,a.id as aId, a.street as aST, a.houseNr as aHO, a.bus as aBU, a.city as aCI, a.postalCode as aPo from Visit v join Visitor vi on v.visitorId = vi.id join Employee e on v.employeeId = e.id join Company c on v.companyId = c.id join Address a on c.addressId = a.id where v.visitId = @visitId and v.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitId", id);
                    Visit visit = null;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int employeeId = (int)reader["eId"];
                        string employeeName = (string)reader["eFN"];
                        string employeeLastName = (string)reader["eLA"];
                        string employeeEmail = (string)reader["eEM"];
                        string employeeFunction = (string)reader["eOC"];
                        int addressId = (int)reader["aID"];
                        string city = (string)reader["aCI"];
                        string postalCode = (string)reader["aPo"];
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


                        Employee employee = EmployeeFactory.MakeEmployee(employeeId, employeeName, employeeLastName, employeeEmail, employeeFunction,companyId);
                        Address address = new Address(addressId, city,postalCode, street, houseNr, busNr);
                        Company company = CompanyFactory.MakeCompany(companyId, companyName, vatNo,address,telNo,companyEmail);

                        Visitor visitor = VisitorFactory.MakeVisitor(visitorId, visitorName, visitorEmail, visitorCompany);
                        visit = VisitFactory.MakeVisit(visitId, visitor, company, employee);
                    }
                    return visit;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method sets the EndTime property of a vist.
        /// </summary>
        /// <param name="email">The e-mail address of the visitor.</param>
        /// <param name="endTime">The end time of the visit.</param>
        /// <returns>The amount of rows affected by the query.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public int EndVisit(string email, DateTime endTime)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"UPDATE Visit
                            JOIN Visitor ON Visit.visitorId = Visitor.id
                            SET Visit.endTime = @endTime
                            WHERE Visitor.email = @email AND Visit.endTime IS NULL;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@endTime", endTime);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("EndVisit", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves all visits from the database.
        /// </summary>
        /// <returns>A readonly list containing all the visits from the database.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<VisitDTO> GetVisits()
        {
            List<VisitDTO> visits = new List<VisitDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select v.visitId as vVI, v.startTime as vST, v.endTime as vEN, vi.id as viI, vi.name as viN, vi.email as viE, vi.visitorCompany as viV, e.firstName as eFN, e.lastName as eLA, c.name as cNA from Visit v join Visitor vi on v.visitorId = vi.id join Employee e on v.employeeId = e.id join Company c on v.companyId = c.id join Address a on c.addressId = a.id where v.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string employeeName = (string)reader["eFN"];
                        string employeeLastName = (string)reader["eLA"];
                        string companyName = (string)reader["cNA"];

                        int visitorId = (int)reader["viI"];
                        string visitorName = (string)reader["viN"];
                        string visitorEmail = (string)reader["viE"];
                        string visitorCompany = (string)reader["viV"];

                        int visitId = (int)reader["vVi"];
                        DateTime startTime = (DateTime)reader["vST"];
                        DateTime? endTime = null;
                        if (reader["vEN"] != DBNull.Value)
                        {
                            endTime = (DateTime)reader["vEN"];
                        }
                        string employee = employeeName + " " + employeeLastName;
                        string company = companyName;

                        Visitor visitor = VisitorFactory.MakeVisitor(visitorId, visitorName, visitorEmail, visitorCompany);
                        VisitDTO visit = new VisitDTO(visitId, visitor, startTime, endTime, company, employee);

                        visits.Add(visit);
                    }
                    reader.Close();
                    return visits;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisits");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method adds a visitor to the database. It then retrieves the inserted
        /// ID and sets the ID property of the visitor.
        /// </summary>
        /// <param name="visitor">The visitor to add.</param>
        /// <returns>The inserted visitor object with the assigned ID.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Visitor AddVisitor(Visitor visitor)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"INSERT INTO Visitor (name,email,visitorCompany) VALUES (@name,@email,@visitorCompany)";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", visitor.Name);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visitor.VisitorCompany);
                    cmd.ExecuteNonQuery();
                    int insertedId = (int)cmd.LastInsertedId;
                    visitor.SetId(insertedId);
                    return visitor;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("AddVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method removes a visitor from the database.
        /// </summary>
        /// <param name="id">The ID of the visitor to remove.</param>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void RemoveVisitor(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Visitor t1 join Visit t2 on t1.id = t2.visitorId set t1.visible = 0, t2.visible = 0 where t1.id = @id;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("RemoveVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method updates a visitor in the database.
        /// </summary>
        /// <param name="visitor">The updated visitor object.</param>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public void UpdateVisitor(Visitor visitor)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"UPDATE Visitor SET name = @name, email = @email, visitorCompany = @visitorCompany WHERE id = @id";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", visitor.Name);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visitor.VisitorCompany);
                    cmd.Parameters.AddWithValue("@id", visitor.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("UpdateVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain visitor exists in the database.
        /// </summary>
        /// <param name="visitor">The visitor to check.</param>
        /// <returns>A bool indiciating whether the visitor exists in the database or not.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool VisitorExists(Visitor visitor)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Visitor WHERE email=@email and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("VisitorExists by email", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method checks whether a certain visitor exists in the database.
        /// </summary>
        /// <param name="id">The ID of the visitor to check.</param>
        /// <returns>A bool indiciating whether the visitor exists in the database or not.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool VisitorExists(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Visitor WHERE id=@id and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("VisitorExists by id", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        
        /// <summary>
        /// This method checks whether a certain visitor exists in the database.
        /// </summary>
        /// <param name="email">The e-mail address of the visitor to check.</param>
        /// <returns>A bool indiciating whether the visitor exists in the database or not.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public bool VisitorExists(string email) {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM Visitor WHERE email = @email and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", email);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("VisitorExists by id", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves a visitor from the database.
        /// </summary>
        /// <param name="id">The ID of the visitor.</param>
        /// <returns>The visitor object matching the given ID.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Visitor GetVisitor(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT * FROM Visitor WHERE id=@id and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Visitor visitor = VisitorFactory.MakeVisitor((int)reader["id"], (string)reader["name"], (string)reader["email"], (string)reader["visitorCompany"]);
                    reader.Close();
                    return visitor;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisitor");
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        /// <summary>
        /// This method retrieves a visitor from the database.
        /// </summary>
        /// <param name="email">The e-mail of the visitor.</param>
        /// <returns>The visitor object matching the given ID.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public Visitor GetVisitor(string email)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT * FROM Visitor WHERE email=@email and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", email);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Visitor visitor = VisitorFactory.MakeVisitor((int)reader["id"], (string)reader["name"], (string)reader["email"], (string)reader["visitorCompany"]);
                    reader.Close();
                    return visitor;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisitor");
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        /// <summary>
        /// This method retrieves all the visitors from the database.
        /// </summary>
        /// <returns>A readonly list containing all the visitors from the database.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public List<Visitor> GetAllVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"SELECT * FROM Visitor where visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        visitors.Add(VisitorFactory.MakeVisitor((int)reader["id"], (string)reader["name"], (string)reader["email"], (string)reader["visitorCompany"]));
                    }
                    reader.Close();
                    return visitors;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisitors");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method retrieves the visits from the database, given the visitor.
        /// </summary>
        /// <param name="visitordId">The ID of the visitor.</param>
        /// <returns>A readonly list containing the matched visits.</returns>
        /// <exception cref="VisitRepositoryADOException">
        /// Thrown when any exception gets caught between opening the 
        /// connection to the database and executing the query.
        /// </exception>
        public IReadOnlyList<VisitDTO> GetVisitsByVisitorId(int visitordId)
        {
            List<VisitDTO> visits = new List<VisitDTO>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select v.visitId as vVI, v.startTime as vST, v.endTime as vEN, vi.id as viI, vi.name as viN, vi.email as viE, vi.visitorCompany as viV, e.firstName as eFN, e.lastName as eLA, c.name as cNA from Visit v join Visitor vi on v.visitorId = vi.id join Employee e on v.employeeId = e.id join Company c on v.companyId = c.id join Address a on c.addressId = a.id where v.visible = 1 and visitorId = @id order by visitId";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", visitordId);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string employeeName = (string)reader["eFN"];
                        string employeeLastName = (string)reader["eLA"];
                        string companyName = (string)reader["cNA"];

                        int visitorId = (int)reader["viI"];
                        string visitorName = (string)reader["viN"];
                        string visitorEmail = (string)reader["viE"];
                        string visitorCompany = (string)reader["viV"];

                        int visitId = (int)reader["vVi"];
                        DateTime startTime = (DateTime)reader["vST"];
                        DateTime? endTime = null;
                        if (reader["vEN"] != DBNull.Value)
                        {
                            endTime = (DateTime)reader["vEN"];
                        }
                        string employee = employeeName + " " + employeeLastName;
                        string company = companyName;

                        Visitor visitor = VisitorFactory.MakeVisitor(visitorId, visitorName, visitorEmail, visitorCompany);
                        VisitDTO visit = new VisitDTO(visitId, visitor, startTime, endTime, company, employee);

                        visits.Add(visit);
                    }
                    reader.Close();
                    return visits;
                }
                catch (Exception ex)
                {
                    throw new VisitRepositoryADOException("GetVisits");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
