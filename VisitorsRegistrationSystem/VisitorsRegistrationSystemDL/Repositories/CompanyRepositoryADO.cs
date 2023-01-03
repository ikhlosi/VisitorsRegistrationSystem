using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;
using VisitorsRegistrationSystemDL.Exceptions;
using System.ComponentModel.Design;

namespace VisitorsRegistrationSystemDL.Repositories
{
    public class CompanyRepositoryADO : ICompanyRepository
    {
        private string connectionString;

        public CompanyRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CompanyExistsInDB(Company company)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Company where VAT= @VAT AND visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@VAT", company.VATNumber);
                    // Query executen
                    Int64 n = (Int64)cmd.ExecuteScalar();
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
        public bool CompanyExistsInDB(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Company where id= @id AND visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    // Parameters adden
                    cmd.Parameters.AddWithValue("@id", iD);
                    // Query executen
                    Int64 n = (Int64)cmd.ExecuteScalar();
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
        public IReadOnlyList<Company> GetCompaniesFromDB()
        {
            List<Company> companies = new List<Company>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, a.id as aId,city,postalCode, street, houseNr, bus from Company c join Address a on c.addressId = a.id where c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        int addressId = (int)reader["aId"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        Company company = CompanyFactory.MakeCompany(id, name, VAT, new Address(addressId, city, postalCode, street, houseNr, busNr), telNr, email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, postalCode, street, houseNr, bus from Company c join Address a on c.addressId = a.id where c.id = @id and c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    Company company = null;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, name, VAT, new Address(city, postalCode, street, houseNr, busNr), telNr, email);
                    }
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where name = @name AND c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), telNr, email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where VAT = @VAT AND c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), telNr, email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string queryBusNULL = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where houseNr = @houseNr and street = @street and city = @city and bus is null AND c.visible = 1";
            string queryBusNOTNULL = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where houseNr = @houseNr and street = @street and city = @city and bus = @busNr AND c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int iD = (int)reader["id"];
                            string Name = (string)reader["name"];
                            string VAT = (string)reader["VAT"];
                            string email = (string)reader["email"];
                            string TelNr = (string)reader["telNr"];
                            string city = (string)reader["city"];
                            string postalCode = (string)reader["postalCode"];
                            string street = (string)reader["street"];
                            string houseNr = (string)reader["houseNr"];
                            string busNr = "";
                            company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), TelNr, email);
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
                        cmd.Parameters.AddWithValue("@busNr", address.BusNumber);
                        // Query executen
                        // Data lezen
                        Company company = null;
                        List<Company> companies = new List<Company>();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int iD = (int)reader["id"];
                            string Name = (string)reader["name"];
                            string VAT = (string)reader["VAT"];
                            string email = (string)reader["email"];
                            string TelNr = (string)reader["telNr"];
                            string city = (string)reader["city"];
                            string postalCode = (string)reader["postalCode"];
                            string street = (string)reader["street"];
                            string houseNr = (string)reader["houseNr"];
                            string busNr = (string)reader["bus"];
                            company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), TelNr, email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where telNr = @telNr AND c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string TelNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), TelNr, email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id where email = @email AND c.visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int iD = (int)reader["id"];
                        string Name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string Email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string postalCode = (string)reader["postalCode"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value)
                        {
                            busNr = (string)reader["bus"];
                        }
                        company = CompanyFactory.MakeCompany(iD, Name, VAT, new Address(city, postalCode, street, houseNr, busNr), telNr, Email);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"UPDATE company c JOIN address a ON c.addressId = a.id SET c.visible=0, a.visible=0 WHERE c.id = @id AND c.visible=1";
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"UPDATE company c JOIN address a ON c.addressId = a.id SET c.name = @name, c.VAT = @VAT, c.email = @email, c.telNr = @telNr, a.street = @street, a.houseNr = @houseNr, a.bus = @bus, a.postalCode = @postCode, a.city = @city WHERE c.id = @id;";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    cmd.Parameters.AddWithValue("@street", company.Address.Street);
                    cmd.Parameters.AddWithValue("@houseNr", company.Address.HouseNumber);
                    cmd.Parameters.AddWithValue("@bus", company.Address.BusNumber);
                    cmd.Parameters.AddWithValue("@postCode", company.Address.PostalCode);
                    cmd.Parameters.AddWithValue("@city", company.Address.City);
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
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"INSERT into Company (name,vat,email,telNr,addressId) values (@name,@VAT,@email,@telNr,@addressId)";
            using (MySqlCommand cmd = connection.CreateCommand())
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

        public bool EmployeeExistsInDB(Employee employee)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Employee where firstName=@name and lastName=@lastname AND visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    return (n > 0);
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("EmployeeExistsInDBname", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public bool EmployeeExistsInDB(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select count(*) from Employee where id=@id AND visible = 1;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    Int64 n = (Int64)cmd.ExecuteScalar();
                    return (n > 0);
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("EmployeeExistsInDBid", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public Employee GetEmployee(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from Employee where id=@id AND visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", iD);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int id = (int)reader["id"];
                    string fname = (string)reader["firstName"];
                    string lname = (string)reader["lastName"];
                    string email = null;
                    if (reader["email"] != DBNull.Value) email = (string)reader["email"];
                    string function = (string)reader["occupation"];
                    int companyId = (int)reader["companyId"];

                    Employee employee = EmployeeFactory.MakeEmployee(id, fname, lname, email, function, companyId);
                    return employee;
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetEmployeeByIdFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Employee> GetEmployeesFromDB()
        {
            List<Employee> employees = new List<Employee>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from Employee where visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string fname = (string)reader["firstName"];
                        string lname = (string)reader["lastName"];
                        int companyId = (int)reader["companyId"];
                        string email = null;
                        if (reader["email"] != DBNull.Value) email = (string)reader["email"];
                        string function = (string)reader["occupation"];

                        Employee employee = EmployeeFactory.MakeEmployee(id, fname, lname, email, function, companyId);
                        employees.Add(employee);
                    }
                    return employees.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetEmployeesFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void RemoveEmployeeFromDB(int iD)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update employee set visible=0 where id = @id and visible=1";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    throw new CompanyRepositoryADOException("RemoveEmployeeFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateEmployeeInDB(Employee employee, Company company)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"update Employee set firstName=@name, lastName=@lastname, email=@email, occupation=@function,companyId = @companyId where id=@id;";
            using (MySqlCommand cmd = connection.CreateCommand())
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
                    cmd.Parameters.AddWithValue("@companyId", company.ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("UpdateEmployeeInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void WriteEmployeeInDB(Employee employee, Company company)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"insert into Employee (firstName, lastName, email, occupation,companyId) values (@name, @lastname, @email, @function, @companyId);";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("@email", employee.Email);
                    cmd.Parameters.AddWithValue("@function", employee.Function);
                    cmd.Parameters.AddWithValue("@companyId", company.ID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("WriteEmployeeInDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Employee> GetEmployeesFromCompanyIdDB(int companyId)
        {
            List<Employee> employees = new List<Employee>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = @"select * from Employee WHERE companyId=@companyId and visible = 1";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", companyId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string fname = (string)reader["firstName"];
                        string lname = (string)reader["lastName"];
                        string email = null;
                        if (reader["email"] != DBNull.Value) email = (string)reader["email"];
                        string function = (string)reader["occupation"];


                        Employee employee = EmployeeFactory.MakeEmployee(id, fname, lname, email, function, companyId);
                        employees.Add(employee);
                    }
                    return employees.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new CompanyRepositoryADOException("GetEmployeesFromCompanyIdDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

