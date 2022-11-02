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

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class CompanyRepositoryADO : ICompanyRepository
    {
        private string connectionString;

        public CompanyRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        // DONE
        public bool CompanyExistsInDB(Company company)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Company where VAT= @VAT;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@VAT", company.VATNumber);
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
                    throw new CompanyRepositoryADOException("CompanyExistsInDBVAT", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        // DONE
        public bool CompanyExistsInDB(int iD)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select count(*) from Company where id= @id;";
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
                    throw new CompanyRepositoryADOException("CompanyExistsInDBid", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        // DONE
        public IReadOnlyList<Company> GetCompaniesFromDB()
        {
            List<Company> companies = new List<Company>();

            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        Company company = CompanyFactory.MakeCompany(id,name,VAT,new Address(city,street,houseNr,busNr),telNr,email);
                        companies.Add(company);
                    }
                    reader.Close();
                    return companies;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        public IEnumerable<Company> GetCompaniesFromDB(string name, string vatNum, Address address, string telNumber, string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    // Query executen
                    // Data lezen
                    // Value returnen
                    return null;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("CompanyExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Company GetCompany(int iD)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    // Query executen
                    // Data lezen
                    // Value returnen
                    return null;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("CompanyExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveCompanyFromDB(Company company)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    // Data lezen
                    // Query executen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("CompanyExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateCompanyInDB(Company company)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    // Data lezen
                    // Query executen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("CompanyExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void WriteCompanyInDB(Company company)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    // Data lezen
                    // Query executen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("CompanyExistsInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
