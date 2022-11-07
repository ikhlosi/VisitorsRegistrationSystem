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
        
        public Company GetCompanyByIdFromDB(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where c.id = @id";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@id", id);
                    // Query executen
                    // Data lezen
                    Company company = null;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
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
                        company = CompanyFactory.MakeCompany(iD, name, VAT, new Address(city, street, houseNr, busNr), telNr, email);
                    }
                    // Value returnen
                    return company;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompanyByIdFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Company> GetCompaniesByNameFromDB(string name)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where name = @name";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@name", name);
                    // Query executen
                    // Data lezen
                    Company company = null;
                    List<Company> companies = new List<Company>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
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
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), telNr, email);
                        companies.Add(company);
                    }
                    // Value returnen
                    return companies;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesByNameFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Company> GetCompaniesByVatnumFromDB(string vatNum)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where VAT = @VAT";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@VAT", vatNum);
                    // Query executen
                    // Data lezen
                    Company company = null;
                    List<Company> companies = new List<Company>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
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
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), telNr, email);
                        companies.Add(company);
                    }
                    // Value returnen
                    return companies;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesByVatnumFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Company> GetCompaniesByAddressFromDB(Address address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryBusNULL = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where houseNr = @houseNr and street = @street and city = @city and bus is null";
            string queryBusNOTNULL = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where houseNr = @houseNr and street = @street and city = @city and bus = @busNr";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    if (address.BusNumber == null)
                    {
                        connection.Open();
                        cmd.CommandText = queryBusNULL;
                        // Parameters adden
                        cmd.Parameters.AddWithValue("@houseNr", address.HouseNumber);
                        cmd.Parameters.AddWithValue("@street", address.Street);
                        cmd.Parameters.AddWithValue("@city", address.City);
                        // Query executen
                        // Data lezen
                        Company company = null;
                        List<Company> companies = new List<Company>();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int iD = (int)reader["id"];
                            string Name = (string)reader["name"];
                            string VAT = (string)reader["VAT"];
                            string email = (string)reader["email"];
                            string TelNr = (string)reader["telNr"];
                            string city = (string)reader["city"];
                            string street = (string)reader["street"];
                            string houseNr = (string)reader["houseNr"];
                            string busNr = "";
                            company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), TelNr, email);
                            companies.Add(company);
                        }
                        return companies;
                    }
                    else
                    {
                        connection.Open();
                        cmd.CommandText = queryBusNOTNULL;
                        // Parameters adden
                        cmd.Parameters.AddWithValue("@houseNr", address.HouseNumber);
                        cmd.Parameters.AddWithValue("@street", address.Street);
                        cmd.Parameters.AddWithValue("@city", address.City);
                        cmd.Parameters.AddWithValue("@busNr",address.BusNumber);
                        // Query executen
                        // Data lezen
                        Company company = null;
                        List<Company> companies = new List<Company>();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int iD = (int)reader["id"];
                            string Name = (string)reader["name"];
                            string VAT = (string)reader["VAT"];
                            string email = (string)reader["email"];
                            string TelNr = (string)reader["telNr"];
                            string city = (string)reader["city"];
                            string street = (string)reader["street"];
                            string houseNr = (string)reader["houseNr"];
                            string busNr = (string)reader["bus"];
                            company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), TelNr, email);
                            companies.Add(company);
                        }
                        return companies;
                    }
                    // Value returnen
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesByAddressFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Company> GetCompaniesByTelnrFromDB(string telNr)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where telNr = @telNr";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@telNr", telNr);
                    // Query executen
                    // Data lezen
                    Company company = null;
                    List<Company> companies = new List<Company>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string TelNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), TelNr, email);
                        companies.Add(company);
                    }
                    // Value returnen
                    return companies;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesByTelNrFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Company> GetCompaniesByEmailFromDB(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where email = @email";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@email", email);
                    // Query executen
                    // Data lezen
                    Company company = null;
                    List<Company> companies = new List<Company>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string Email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, street, houseNr, busNr), telNr, Email);
                        companies.Add(company);
                    }
                    // Value returnen
                    return companies;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetCompaniesByEmailFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void RemoveCompanyFromDB(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"DELETE FROM company WHERE id = @id";
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
                    throw new CompanyRepositoryADOException("RemoveCompanyFromDB", ex);
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
            string query = @"UPDATE Company set name = @name, VAT = @VAT, email = @email, telNr = @telNr ,addressId = @addressId where id = @id;";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@name",company.Name);
                    cmd.Parameters.AddWithValue("@VAT",company.VATNumber);
                    cmd.Parameters.AddWithValue("@email", company.Email);
                    cmd.Parameters.AddWithValue("@telNr", company.TelephoneNumber);
                    cmd.Parameters.AddWithValue("@addressId", company.Address.Id);
                    cmd.Parameters.AddWithValue("@id", company.ID);
                    // Query executen
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("UpdateCompanyInDB", ex);
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
            string query = @"INSERT into Company (name,vat,email,telNr,addressId) values (@name,@VAT,@email,@telNr,@addressId)";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@name", company.Name);
                    cmd.Parameters.AddWithValue("@VAT", company.VATNumber);
                    cmd.Parameters.AddWithValue("@email", company.Email);
                    cmd.Parameters.AddWithValue("@telNr", company.TelephoneNumber);
                    cmd.Parameters.AddWithValue("@addressId", company.Address.Id);
                    // Query executen
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("WriteCompanyInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}