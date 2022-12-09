using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers {
    public class CompanyManager {
        public CompanyManager(ICompanyRepository repo) {
            _repo = repo;
        }
        private ICompanyRepository _repo;
        public void AddCompany(Company company) {
            if (company == null) {
                throw new CompanyException("CompanyManager - AddCompany - company is null.");
            }
            try {
                if (_repo.CompanyExistsInDB(company)) throw new CompanyException("CompanyManager - AddCompany - company already exists in DB.");
                _repo.WriteCompanyInDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - AddCompany", ex);
            }
        }
        public void RemoveCompany(Company company) {
            if (company == null) throw new CompanyException("CompanyManager - RemoveCompany - company is null.");
            
            try {
                if (!_repo.CompanyExistsInDB(company.ID)) throw new CompanyException("CompanyManager - RemoveCompany - company does not exist in DB.");
                _repo.RemoveCompanyFromDB(company.ID);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - RemoveCompany", ex);
            }
        }
        public void UpdateCompany(Company company) {
            if (company == null) throw new CompanyException("CompanyManager - UpdateCompany - company is null.");
            try {
                if (!_repo.CompanyExistsInDB(company.ID)) throw new CompanyException("CompanyManager - UpdateCompany - company does not exist in DB.");
                Company companyDb = _repo.GetCompanyByIdFromDB(company.ID);
                if (companyDb.IsSame(company)) throw new CompanyException("CompanyManager - UpdateCompany - fields are the same, nothing to update.");
                _repo.UpdateCompanyInDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - UpdateCompany", ex);
            }
        }
        public IReadOnlyList<Company> GetCompanies() {
            try {
                return _repo.GetCompaniesFromDB();
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - GetCompanies", ex);
            }
        }
        public IReadOnlyList<Company> SearchCompany(int? id, string name, string vatNum, Address address, string telNumber, string email) {
            List<Company> companies = new List<Company>();
            try {
                if (id.HasValue && _repo.CompanyExistsInDB(id.Value)) companies.Add(_repo.GetCompanyByIdFromDB(id.Value));
                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(vatNum) || address != null || !string.IsNullOrWhiteSpace(telNumber) || !string.IsNullOrWhiteSpace(email))
                {
                    // companies.AddRange(_repo.GetCompaniesFromDB(name, vatNum, address, telNumber, email));
                    companies.Add(_repo.GetCompanyByIdFromDB(id.Value));
                    companies.AddRange(_repo.GetCompaniesByNameFromDB(name));
                    companies.AddRange(_repo.GetCompaniesByVatnumFromDB(vatNum));
                    companies.AddRange(_repo.GetCompaniesByAddressFromDB(address));
                    companies.AddRange(_repo.GetCompaniesByTelnrFromDB(telNumber));
                    companies.AddRange(_repo.GetCompaniesByEmailFromDB(email));

                }
                return companies;
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - SearchCompany", ex);
            }
        }

        public void AddEmployee(Employee employee, Company company)
        {
            if (employee == null) throw new CompanyException("CompanyManager - Addemployee - employee is null");
            try
            {
                // if (_repo.EmployeeExistsInDB(employee)) throw new EmployeeException("EmployeeManager - AddEmployee - employee already exists in DB.");
                // this check will be done in UI, through a pop-up windows asking if the user is sure he wants to add 
                // 2 employees with the same name
                _repo.WriteEmployeeInDB(employee,company);
            }
            catch (Exception ex)
            {
                throw new CompanyException("CompanyManager - AddEmployee", ex);
            }
        }
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null) throw new CompanyException("CompanyManager - RemoveEmployee - employee is null");
            try
            {
                if (!_repo.EmployeeExistsInDB(employee.ID)) throw new CompanyException("CompanyManager - RemoveEmployee - employee does not exist in DB.");
                _repo.RemoveEmployeeFromDB(employee.ID);
            }
            catch (Exception ex)
            {
                throw new CompanyException("CompanyManager - RemoveEmployee", ex);
            }
        }
        public void UpdateEmployee(Employee employee, Company company)
        {
            if (employee == null) throw new CompanyException("CompanyManager - UpdateEmployee - employee is null");
            try
            {
                if (!_repo.EmployeeExistsInDB(employee.ID)) throw new CompanyException("CompanyManager - UpdateEmployee - employee does not exist in DB.");
                Employee employeeDb = _repo.GetEmployee(employee.ID);
                if (employeeDb.IsSame(employee)) throw new CompanyException("CompanyManager - UpdateEmployee - fields are the same, nothing to update.");
                _repo.UpdateEmployeeInDB(employee,company);
            }
            catch (Exception ex)
            {
                throw new CompanyException("CompanyManager - UpdateEmployee", ex);
            }
        }
        public IReadOnlyList<Employee> GetEmployees()
        {
            try
            {
                return _repo.GetEmployeesFromDB();
            }
            catch (Exception ex)
            {
                throw new CompanyException("CompanyManager - GetEmployees", ex);
            }
        }

        public IReadOnlyList<Employee> GetEmployeesFromCompanyId(int companyId)
        {
            try
            {
                return _repo.GetEmployeesFromCompanyIdDB(companyId);
            }
            catch (Exception ex)
            {
                throw new CompanyException("CompanyManager - GetEmployees", ex);
            }
        }
    }
}
// TODO: finish CompanyManager documentation
