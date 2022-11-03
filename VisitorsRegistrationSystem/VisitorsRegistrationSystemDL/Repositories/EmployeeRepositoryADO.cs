using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;

namespace VisitorsRegistrationSystemDL.Repositories {
    public class EmployeeRepositoryADO : IEmployeeRepository {
        private string connectionString;

        public EmployeeRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool EmployeeExistsInDB(Employee employee) {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Employee where firstName=@name and lastName=@lastname;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    int n = (int)cmd.ExecuteScalar();
                    return (n > 0);
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("EmployeeRepositoryADO - EmployeeExistsInDBname", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool EmployeeExistsInDB(int iD) {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Employee where id=@id;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    int n = (int)cmd.ExecuteScalar();
                    return (n > 0);
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("EmployeeRepositoryADO - EmployeeExistsInDBid", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Employee GetEmployee(int iD) {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select * from Employee where id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int id = (int)reader["id"];
                    string fname = (string)reader["firstName"];
                    string lname = (string)reader["lastName"];
                    string email = null;
                    if (reader["email"] != DBNull.Value) email = (string)reader["email"];
                    string function = (string)reader["occupation"];
                    
                    Employee employee = EmployeeFactory.MakeEmployee(id, fname, lname, email, function);
                    return employee;
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("GetEmployeeByIdFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Employee> GetEmployeesFromDB() {
            List<Employee> employees = new List<Employee>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select * from Employee";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int id = (int)reader["id"];
                        string fname = (string)reader["firstName"];
                        string lname = (string)reader["lastName"];
                        string email = null;
                        if (reader["email"] != DBNull.Value) email = (string)reader["email"];
                        string function = (string)reader["occupation"];
                        
                        Employee employee = EmployeeFactory.MakeEmployee(id, fname, lname, email, function);
                        employees.Add(employee);
                    }
                    return employees.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("GetEmployeesFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveEmployeeFromDB(int iD) {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"delete from Employee where id=@id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("RemoveEmployeeFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateEmployeeInDB(Employee employee) {
            // todo: Employee doesn't know about a CompanyId
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"update Employee set firstName=@name, lastName=@lastname, email=@email, occupation=@function, where id=@id;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("@email", employee.Email);
                    cmd.Parameters.AddWithValue("@function", employee.Function);
                    cmd.Parameters.AddWithValue("@id", employee.ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("UpdateEmployeeInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void WriteEmployeeInDB(Employee employee) {
            // todo: Employee doesn't know about a CompanyId
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"insert into Employee (firstName, lastName, email, occupation) values (@name, @lastname, @email, @function);";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("@email", employee.Email);
                    cmd.Parameters.AddWithValue("@function", employee.Function);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepositoryADOException("WriteEmployeeInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
