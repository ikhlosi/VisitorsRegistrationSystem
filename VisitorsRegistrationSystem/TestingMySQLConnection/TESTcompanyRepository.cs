using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Factories;
using VisitorsRegistrationSystemBL.Interfaces;

namespace TestingMySQLConnection {
    public class TESTcompanyRepository : ICompanyRepository {
        private string _connectionString;
        public TESTcompanyRepository(string connectionString) {
            this._connectionString = connectionString;
        }
        public bool CompanyExistsInDB(Company company) {
            throw new NotImplementedException();
        }

        public bool CompanyExistsInDB(int iD) {
            throw new NotImplementedException();
        }

        public bool EmployeeExistsInDB(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompaniesByAddressFromDB(Address address) {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompaniesByEmailFromDB(string email) {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompaniesByNameFromDB(string name) {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompaniesByTelnrFromDB(string telNr) {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompaniesByVatnumFromDB(string vatNum) {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Company> GetCompaniesFromDB() {
            List<Company> companies = new List<Company>();
            string query = "select c.id,name,VAT,email,telNr, city, street, houseNr, bus from Company c join Address a on c.addressId = a.id";
            MySqlConnection conn = new MySqlConnection(_connectionString);
            using (MySqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int id = (int)reader["id"];
                        string name = (string)reader["name"];
                        string VAT = (string)reader["VAT"];
                        string email = (string)reader["email"];
                        string telNr = (string)reader["telNr"];
                        string city = (string)reader["city"];
                        string street = (string)reader["street"];
                        string houseNr = (string)reader["houseNr"];
                        string busNr = "";
                        if (reader["bus"] != DBNull.Value) {
                            busNr = (string)reader["bus"];
                        }
                        Company company = CompanyFactory.MakeCompany(id, name, VAT, new Address(city, street, houseNr, busNr), telNr, email);
                        companies.Add(company);
                    }
                    reader.Close();
                    return companies;
                }
                catch (Exception ex) {
                    throw;
                }
                finally { conn.Close(); }

            }

        }

        public Company GetCompanyByIdFromDB(int id) {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int iD) {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Employee> GetEmployeesFromCompanyIdDB(int companyId) {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Employee> GetEmployeesFromDB() {
            throw new NotImplementedException();
        }

        public void RemoveCompanyFromDB(int id) {
            throw new NotImplementedException();
        }

        public void RemoveEmployeeFromDB(int iD) {
            throw new NotImplementedException();
        }

        public void UpdateCompanyInDB(Company company) {
            throw new NotImplementedException();
        }

        public void UpdateEmployeeInDB(Employee employee, Company company) {
            throw new NotImplementedException();
        }

        public void WriteCompanyInDB(Company company) {
            throw new NotImplementedException();
        }

        public void WriteEmployeeInDB(Employee employee, Company company) {
            throw new NotImplementedException();
        }
    }
}
