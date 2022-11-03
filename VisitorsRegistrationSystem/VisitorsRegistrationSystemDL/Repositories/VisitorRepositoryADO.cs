using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;
using VisitorsRegistrationSystemBL.Factories;

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class VisitorRepositoryADO : IVisitorRepository
    {
        private string connectionString;

        public VisitorRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddVisitor(Visitor visitor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"INSERT INTO visitor (name,email,visitorCompany) VALUES (@name,@email,@visitorCompany)";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", visitor.Name);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visitor.VisitorCompany);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("AddVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void RemoveVisitor(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"DELETE FROM visitor WHERE id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
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
                    throw new CompanyRepositoryADOException("RemoveVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateVisitor(Visitor visitor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"UPDATE visitor SET name = @name, email = @email, visitorCompany = @visitorCompany WHERE id = @id";
            using (SqlCommand cmd = connection.CreateCommand())
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
                    throw new CompanyRepositoryADOException("UpdateVisitor", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool VisitorExists(Visitor visitor)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM visitor WHERE email=@email";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("VisitorExists by email", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool VisitorExists(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT COUNT(*) FROM visitor WHERE id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("VisitorExists by id", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Visitor GetVisitor(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM visitor WHERE id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Visitor visitor = VisitorFactory.MakeVisitor((int)reader["id"],(string)reader["name"], (string)reader["email"], (string)reader["visitorCompany"]);
                    reader.Close();
                    return visitor;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepositoryException("GetVisitor");
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public List<Visitor> GetAllVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"SELECT * FROM visitor";
            using (SqlCommand cmd = connection.CreateCommand())
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
                    throw new VisitorRepositoryException("GetVisitors");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
