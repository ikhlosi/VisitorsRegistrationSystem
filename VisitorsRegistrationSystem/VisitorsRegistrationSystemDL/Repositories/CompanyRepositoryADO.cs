using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
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

        public bool CompanyExistsInDB(Company company)
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
                    return false;
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

        public bool CompanyExistsInDB(int iD)
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
                    return false;
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

        public IReadOnlyList<Company> GetCompaniesFromDB()
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
