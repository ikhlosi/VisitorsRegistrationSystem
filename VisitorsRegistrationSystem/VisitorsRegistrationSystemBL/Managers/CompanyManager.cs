using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;
using VisitorsRegistrationSystemBL.Exceptions;
using VisitorsRegistrationSystemBL.Interfaces;

namespace VisitorsRegistrationSystemBL.Managers {
    public class CompanyManager {
        private ICompanyRepository _repo;
        public void AddCompany(Company company) {
            if (company == null) {
                CompanyException ex = new CompanyException("CompanyManager - AddCompany - company is null.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            if (_repo.CompanyExistsInDB(company)) {
                CompanyException ex = new CompanyException("CompanyManager - AddCompany - company already exists in DB.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            _repo.WriteCompanyInDB(company);
        }
        public void RemoveCompany(Company company) {
            if (company == null) {
                CompanyException ex = new CompanyException("CompanyManager - RemoveCompany - company is null.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            if (!_repo.CompanyExistsInDB(company)) {
                CompanyException ex = new CompanyException("CompanyManager - RemoveCompany - company does not exist in DB.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            _repo.RemoveCompanyFromDB(company);
        }
        public void UpdateCompany(Company company) {
            if (company == null) {
                CompanyException ex = new CompanyException("CompanyManager - UpdateCompany - company is null.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            if (!_repo.CompanyExistsInDB(company)) {
                CompanyException ex = new CompanyException("CompanyManager - UpdateCompany - company does not exist in DB.");
                ex.Data.Add("Company", company);
                throw ex;
            }
            _repo.UpdateCompanyInDB(company);
        }
    }
}
