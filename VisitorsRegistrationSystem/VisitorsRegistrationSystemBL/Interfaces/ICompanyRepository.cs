using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces {
    public interface ICompanyRepository {
        void WriteCompanyInDB(Company company);
        void RemoveCompanyFromDB(int id);
        void UpdateCompanyInDB(Company company);
        bool CompanyExistsInDB(Company company);
        bool CompanyExistsInDB(int iD);
        IReadOnlyList<Company> GetCompaniesFromDB();
        Company GetCompanyByIdFromDB(int id);
        IEnumerable<Company> GetCompaniesByNameFromDB(string name);
        IEnumerable<Company> GetCompaniesByVatnumFromDB(string vatNum);
        IEnumerable<Company> GetCompaniesByAddressFromDB(Address address);
        IEnumerable<Company> GetCompaniesByTelnrFromDB(string telNr);
        bool EmployeeExistsInDB(int id);
        IEnumerable<Company> GetCompaniesByEmailFromDB(string email);
        void WriteEmployeeInDB(Employee employee, Company company);
        void RemoveEmployeeFromDB(int iD);
        void UpdateEmployeeInDB(Employee employee,Company company);
        Employee GetEmployee(int iD);
        IReadOnlyList<Employee> GetEmployeesFromDB();
        IReadOnlyList<Employee> GetEmployeesFromCompanyIdDB(int companyId);
    }
}
