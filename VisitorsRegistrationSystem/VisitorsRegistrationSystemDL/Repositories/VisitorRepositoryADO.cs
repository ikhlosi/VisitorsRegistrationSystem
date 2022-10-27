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
            throw new NotImplementedException();
        }


        public void RemoveVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public bool VisitorExists(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public bool VisitorExists(int iD)
        {
            throw new NotImplementedException();
        }

        public Visitor GetVisitor(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM visitor WHERE id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Visitor visitor = new Visitor((string)reader["name"], (string)reader["email"]);
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
            string query = "SELECT * FROM visitor";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        visitors.Add(new Visitor((string)reader["name"], (string)reader["email"]));
                    }
                    reader.Close();
                    return visitors;
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

    }
}
