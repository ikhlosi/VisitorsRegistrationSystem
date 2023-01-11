using VisitorsRegistrationSystemBL.Checkers;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers {
    public class CompanyManager {
        public CompanyManager(ICompanyRepository repo) {
            _repo = repo;
        }
        private ICompanyRepository _repo;
        /// <summary>
        /// This method throws an exception is company is null and checks if the VAT number and email are valid.
        /// This method also throws an exception if the company already exists in the database.
        /// If no exception is thrown this method adds a new company to the database.
        /// </summary>
        /// <param name="company"></param>
        /// <exception cref="CompanyException"></exception>
        public void AddCompany(Company company) {
            if (company == null) {
                throw new CompanyException("CompanyManager - AddCompany - company is null.");
            }
            if (!VATChecker.IsValid(company.VATNumber)) {
                throw new CompanyException("CompanyManager - AddCompany - invalid VAT");
            }
            if (!EmailChecker.IsValid(company.Email)) {
                throw new CompanyException("CompanyManager - AddCompany - invalid e-mail");
            }
            try {
                if (_repo.CompanyExistsInDB(company)) throw new CompanyException("CompanyManager - AddCompany - company already exists in DB.");
                _repo.WriteCompanyInDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - AddCompany", ex);
            }
        }
        /// <summary>
        /// This method throws an exception if company is null and if the company is not found by id in the database
        /// This method also throws an exception if the company still has employees.
        /// If no exception is thrown the company is removed from the database.
        /// </summary>
        /// <param name="company"></param>
        /// <exception cref="CompanyException"></exception>
        public void RemoveCompany(Company company) {
            if (company == null) throw new CompanyException("CompanyManager - RemoveCompany - company is null.");

            try {
                if (!_repo.CompanyExistsInDB(company.ID)) throw new CompanyException("CompanyManager - RemoveCompany - company does not exist in DB.");
                if (_repo.GetEmployeesFromCompanyIdDB(company.ID).Count > 0) throw new CompanyException("CompanyManager - RemoveCompany - company has employees.");
                _repo.RemoveCompanyFromDB(company.ID);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - RemoveCompany", ex);
            }
        }
        /// <summary>
        /// This method throws an exception if company is null and checks if the VAT number and email are valid.
        /// This method also throws an exception if company is not found by id in the database or when nothing is changed.
        /// If no exception is thrown the company is updated in the database.
        /// </summary>
        /// <param name="company"></param>
        /// <exception cref="CompanyException"></exception>
        public void UpdateCompany(Company company) {
            if (company == null) throw new CompanyException("CompanyManager - UpdateCompany - company is null.");
            if (!VATChecker.IsValid(company.VATNumber))
            {
                throw new CompanyException("CompanyManager - AddCompany - invalid VAT");
            }
            if (!EmailChecker.IsValid(company.Email))
            {
                throw new CompanyException("CompanyManager - AddCompany - invalid e-mail");
            }
            try {
                if (!_repo.CompanyExistsInDB(company.ID)) throw new CompanyException("CompanyManager - UpdateCompany - company does not exist in DB.");
                Company companyDb = _repo.GetCompanyByIdFromDB(company.ID);
                if (companyDb.IsSame(company) && companyDb.Address.Equals(company.Address)) throw new CompanyException("CompanyManager - UpdateCompany - fields are the same, nothing to update.");
                _repo.UpdateCompanyInDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - UpdateCompany", ex);
            }
        }
        /// <summary>
        /// This method returns a company by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CompanyException"></exception>
        public Company GetCompanyById(int id) {
        
            try { 
                return _repo.GetCompanyByIdFromDB(id);
            } catch(Exception ex)
            {
                throw new CompanyException("CompanyManager - GetCompanyById", ex);
            }
        }
        /// <summary>
        /// This method returns a lis of companies.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CompanyException"></exception>
        public IReadOnlyList<Company> GetCompanies() {
            try {
                return _repo.GetCompaniesFromDB();
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - GetCompanies", ex);
            }
        }
        /// <summary>
        /// This method returns a company if found in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="vatNum"></param>
        /// <param name="address"></param>
        /// <param name="telNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="CompanyException"></exception>
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
        /// <summary>
        /// This method throws an exception if the employee is null
        /// If no exception is thrown this method adds a employee with its company to the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="company"></param>
        /// <exception cref="CompanyException"></exception>
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
        /// <summary>
        /// This method throws an exception if employee is null or if the employee is not found by id.
        /// If no exception is thrown the employee if removed.
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="CompanyException"></exception>
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
        /// <summary>
        /// This method throws an exception employee is null.
        /// This method also throws an exception if the employee is not found or if the employee is unchanged
        /// If no exception is thrown the employee is updated in the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="company"></param>
        /// <exception cref="CompanyException"></exception>
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
        /// <summary>
        /// This method returns a list of the employees in the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CompanyException"></exception>
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
        /// <summary>
        /// This method returns a list of the company by companyId
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <exception cref="CompanyException"></exception>
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
