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
                //CompanyException ex = new CompanyException("CompanyManager - AddCompany - company is null.");
                //ex.Data.Add("Company", company);
                //throw ex;
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
    }
}
// TODO: finish CompanyManager documentation
