using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers {
    public class CompanyManager {
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
                _repo.RemoveCompanyFromDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - RemoveCompany", ex);
            }
        }
        public void UpdateCompany(Company company) {
            if (company == null) throw new CompanyException("CompanyManager - UpdateCompany - company is null.");
            try {
                if (!_repo.CompanyExistsInDB(company.ID)) throw new CompanyException("CompanyManager - UpdateCompany - company does not exist in DB.");
                Company companyDb = _repo.GetCompany(company.ID);
                if (companyDb.IsSame(company)) throw new CompanyException("CompanyManager - UpdateCompany - fields are the same, nothing to update.");
                _repo.UpdateCompanyInDB(company);
            }
            catch (Exception ex) {
                throw new CompanyException("CompanyManager - UpdateCompany", ex);
            }
        }
    }
}
