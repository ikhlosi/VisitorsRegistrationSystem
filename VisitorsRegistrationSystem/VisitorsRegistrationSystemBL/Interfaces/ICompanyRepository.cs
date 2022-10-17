﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Domain;

namespace VisitorsRegistrationSystemBL.Interfaces {
    public interface ICompanyRepository {
        void WriteCompanyInDB(Company company);
        void RemoveCompanyFromDB(Company company);
        void UpdateCompanyInDB(Company company);
        bool CompanyExistsInDB(Company company);
        bool CompanyExistsInDB(int iD);
        Company GetCompany(int iD);
        IReadOnlyList<Company> GetCompaniesFromDB();
        IEnumerable<Company> GetCompaniesFromDB(string name, string vatNum, Address address, string telNumber, string email);
    }
}
